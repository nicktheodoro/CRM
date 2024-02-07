using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Product.Core.Models.Product.Validators
{
    internal class ProductModelValidator : AbstractValidator<ProductModel>
    {
        internal ProductModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.UnitPrice)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.UnitInStock)
                .NotEmpty().
                WithMessage(ValidationMessage.NotNull());
        }
    }
}
