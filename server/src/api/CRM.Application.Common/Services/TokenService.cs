using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedDomain;

namespace CRM.Application.Common;

public class TokenService : ITokenService
{
    private readonly IOptions<TokenConfiguration> _configuration;

    public TokenService(IOptions<TokenConfiguration> configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public AccessToken GenerateToken(UserRequest user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("MachineName", Environment.MachineName)
            }),
            Expires = DateTime.UtcNow.AddHours(_configuration.Value.ExpiresIn),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_configuration.Value.GetSecretAsByteArray()),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        DateTimeOffset expiresIn = tokenDescriptor.Expires.Value;

        return new AccessToken
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresIn = int.Parse(expiresIn.ToUnixTimeSeconds().ToString())
        };
    }

    public UserRequest GetUserFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(_configuration.Value.GetSecretAsByteArray()),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false
        };

        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return new UserRequest(
            principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
            principal.Claims.FirstOrDefault(x => x.Type == "Name")?.Value,
            principal.Claims.FirstOrDefault(x => x.Type == "Email")?.Value,
            principal.Claims.FirstOrDefault(x => x.Type == "IpAddress")?.Value
        );
    }

    public string GenerateRefreshToken(UserRequest user)
    {
        var refreshInfo = new RefreshTokenInfo
        {
            ID = user.ID,
            Name = user.Name,
            Email = user.Email,
            IP = Environment.MachineName,
            CreationDate = DateTime.UtcNow
        };

        return TripleDes.Encrypt(_configuration.Value.GetSecretAsByteArray(), JsonSerializer.Serialize(refreshInfo));
    }

    public bool ValidateRefreshToken(string refreshToken, UserRequest user)
    {
        var refreshInfo = JsonSerializer.Deserialize<RefreshTokenInfo>(
            TripleDes.Decrypt(_configuration.Value.GetSecretAsByteArray(), refreshToken)
        );

        return refreshInfo.ID.ToUpper().Equals(user.ID.ToUpper()) &&
               refreshInfo.Name.ToUpper().Equals(user.Name.ToUpper()) &&
               refreshInfo.IP.ToUpper().Equals(Environment.MachineName.ToUpper());
    }

    public bool IsRefreshTokenExpired(string refreshToken)
    {
        var refreshInfo = JsonSerializer.Deserialize<RefreshTokenInfo>(
            TripleDes.Decrypt(_configuration.Value.GetSecretAsByteArray(), refreshToken)
        );

        return refreshInfo.CreationDate.AddHours(_configuration.Value.ExpiresIn * 2) < DateTime.UtcNow;
    }

    public static async Task<UserRequest> GetDatabaseUser(AuthRequest request, string ip, string? conectionString)
    {
        var sql = "SELECT id, name, email, passwordHash FROM users WHERE email = @email";

        var ID = string.Empty;
        var Name = string.Empty;
        var Email = string.Empty;
        var PasswordHash = string.Empty;

        using (var connection = new SqlConnection(conectionString))
        {
            connection.Open();
            var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@email", request.Email.ToLower());

            using SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                ID = reader.GetString(0);
                Name = reader.GetString(1);
                Email = reader.GetString(2);
                PasswordHash = reader.GetString(3);
            }
        }

        if (string.IsNullOrEmpty(Email) || BCrypt.Net.BCrypt.Verify(request.Password, PasswordHash))
            return new UserRequest();

        return new UserRequest(ID, PasswordHash, Name, ip);
    }
}

