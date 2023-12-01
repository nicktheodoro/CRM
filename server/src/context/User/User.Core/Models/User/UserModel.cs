using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.ValueObjects;
using System.Net;
using User.Core.Models.User.Image;
using User.Core.Models.User.Validators;

namespace User.Core.Models.User
{
    public class UserModel : Entity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public string PasswordHash { get => _passwordHash; set => _passwordHash = HashPassword(value); }

        public virtual ImageModel? Image { get; set; }

        private string _passwordHash;

        public void AddImage(IFormFile image)
        {
            if (image == null)
            {
                return;
            }

            if (!image.ContentType.ToLower().StartsWith("image/"))
            {
                throw new ExceptionBase("Invalid image", HttpStatusCode.BadRequest);
            }

            MemoryStream ms = new MemoryStream();
            image.OpenReadStream().CopyTo(ms);

            Image = new ImageModel()
            {
                Content = ms.ToArray(),
                ContentType = image.ContentType,
                UserMaster = this,
            };
        }

        public void InactiveUser()
        {
            IsActive = false;
        }

        public void UpdatePassword(string actualPassWord, string newPassword)
        {
            if (!VerifyPassword(actualPassWord))
            {
                throw new ExceptionBase("Invalid password", HttpStatusCode.Unauthorized);
            }

            PasswordHash = newPassword;
        }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new UserValidator().Validate(this);
            return validationResult.IsValid;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        private bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
