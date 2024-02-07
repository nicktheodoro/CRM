using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Product.Core.Contracts.Commands.Product.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
