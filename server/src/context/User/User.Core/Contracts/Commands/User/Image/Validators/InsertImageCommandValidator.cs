using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace User.Core.Contracts.Commands.User.Image.Validators
{

    public class InsertImageCommandValidator : AbstractValidator<InsertImageCommand>
    {
        public InsertImageCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
