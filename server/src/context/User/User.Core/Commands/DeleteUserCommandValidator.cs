using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace MyApp.Core.Users.Commands
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
