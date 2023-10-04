using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.Core.Users.Commands
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required())
                .EmailAddress()
                .WithMessage("Invalid email.");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
