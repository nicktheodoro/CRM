using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Product.Core.Contracts.Commands.Product.Validators
{
    public class InsertProductCommandValidator : AbstractValidator<InsertProductCommand>
    {
        public InsertProductCommandValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.UnitPrice)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());

            RuleFor(r => r.UnitInStock)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
