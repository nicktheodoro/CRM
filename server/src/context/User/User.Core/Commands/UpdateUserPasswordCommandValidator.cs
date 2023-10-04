using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.Core.Users.Commands
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPassword>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(r => r.ActualPassword)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.NewPassword)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
