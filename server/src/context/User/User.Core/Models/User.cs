using System.Net;
using FluentValidation.Results;
using MyApp.Core.Users.Models;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.ValueObjects;
namespace MyApp.Users.Models
{
    public class User : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;

        public void InactiveUser()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void HashPassword()
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordHash, BCrypt.Net.BCrypt.GenerateSalt());
        }

        public void UpdatePassword(string actualPassWord, string newPassword)
        {
            if (!VerifyPassword(actualPassWord))
            {
                throw new ExceptionBase("Invalid password", HttpStatusCode.Unauthorized);
            }

            PasswordHash = newPassword;
            HashPassword();
            UpdatedAt = DateTime.UtcNow;
        }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new UserValidator().Validate(this);
            return validationResult.IsValid;
        }

        private bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
