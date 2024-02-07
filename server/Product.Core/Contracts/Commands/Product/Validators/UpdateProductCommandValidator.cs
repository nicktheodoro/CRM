using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Product.Core.Contracts.Commands.Product.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
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
