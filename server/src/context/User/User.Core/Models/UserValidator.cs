using FluentValidation;
using MyApp.SharedDomain.Messages;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Models
{
    internal class UserValidator : AbstractValidator<User>
    {
        internal UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessage.NotEmpty());
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidationMessage.NotEmpty());
            RuleFor(x => x.PasswordHash).NotEmpty().WithMessage(ValidationMessage.NotEmpty());
            RuleFor(x => x.IsActive).NotNull().WithMessage(ValidationMessage.NotNull());
        }
    }
}
