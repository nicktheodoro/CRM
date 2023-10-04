using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.Core.Users.Commands
{
    public class InactiveUserCommandValidator : AbstractValidator<InactiveUserCommand>
    {
        public InactiveUserCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
