using CRM.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Application.Controllers;

public class AuthController
{
    [ApiController]
    [Route("/api/auth")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AutenticacaoController(ITokenService tokenService, IConfiguration configuration)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _configuration = configuration;
        }

        /// <summary>Teste de disponibilidade da API</summary>
        /// <remarks>Neste endpoint é possível verificar se a API está disponível.</remarks>
        /// <returns></returns>
        /// <response code="200">pong</response>
        [HttpGet("ping")]
        [AllowAnonymous]
        public string Ping()
        {
            return "pong";
        }

        /// <summary>Requisição do token de acesso</summary>
        /// <remarks>Neste endpoint é possível solicitar o token de acesso à API.</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">{ "accessToken", "refreshToken", "expiresIn", "tokenType" }</response>
        /// <response code="400">{ "mensagem": "Usuário ou senha inválidos" }</response>
        [HttpPost("token")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar([FromForm] AuthRequest request)
        {
            var ipUsuario = "126.0.0.68";

            var usuario = await TokenService.GetDatabaseUser(request, ipUsuario, _configuration.GetConnectionString("DefaultConnection"));

            if (string.IsNullOrEmpty(usuario.ID))
                return BadRequest(new { mensagem = "Usuário ou senha inválidos" });

            var accessToken = _tokenService.GenerateToken(usuario);
            var refreshToken = _tokenService.GenerateRefreshToken(usuario);

            return new TokenResponse()
            {
                AccessToken = accessToken.Token,
                ExpiresIn = accessToken.ExpiresIn,
                RefreshToken = refreshToken
            };
        }

        /// <summary>Verificação da validade do token de acesso</summary>
        /// <remarks>Neste endpoint é possível verificar se o token de acesso ainda é válido.</remarks>                       
        /// <returns></returns>
        /// <response code="200">{ mensagem = "Token válido" }</response>
        /// <response code="401"></response>
        [HttpGet("validate")]
        [Authorize]
        public IActionResult ValidarToken()
        {
            return Ok(new { mensagem = "Token válido" });
        }

        /// <summary>Logout do usuário</summary>
        /// <remarks>Neste endpoint é possível efetuar a desconexão do usuário.</remarks>
        /// <returns></returns>
        /// <response code="200">{ mensagem = "Usuario desconectado!" }</response>
        [HttpDelete("logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            return Ok(new { mensagem = "Usuario desconectado!" });
        }

        /// <summary>Atualização do Token de acesso</summary>
        /// <remarks>Neste endpoint é possível solicitar a atualização do token de acesso à API.</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">{ "accessToken", "refreshToken", "expiresIn", "tokenType" }</response>
        /// <response code="400">{ "mensagem": "Usuário ou refreshToken inválidos" }</response>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public ActionResult<dynamic> Atualizar([FromForm] RefreshTokenRequest request)
        {
            var usuario = _tokenService.GetUserFromToken(request.Token);

            if (!usuario.Email.ToUpper().Equals(request.Email.ToUpper()) || !_tokenService.ValidateRefreshToken(request.RefreshToken, usuario))
            {
                return BadRequest(new { mensagem = "Usuário ou refreshToken inválidos" });
            }

            if (_tokenService.IsRefreshTokenExpired(request.RefreshToken))
            {
                return BadRequest(new { mensagem = "RefreshToken expirado" });
            }

            var accessToken = _tokenService.GenerateToken(usuario);
            var refreshToken = _tokenService.GenerateRefreshToken(usuario);

            return new TokenResponse()
            {
                AccessToken = accessToken.Token,
                ExpiresIn = accessToken.ExpiresIn,
                RefreshToken = refreshToken
            };
        }
    }
}
