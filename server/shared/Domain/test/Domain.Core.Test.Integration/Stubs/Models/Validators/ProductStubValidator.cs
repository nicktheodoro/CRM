using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Domain.Core.Test.Integration.Stubs.Models.Validators
{
    public class ProductStubValidator : AbstractValidator<ProductStubModel>
    {
        internal ProductStubValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.UnitPrice)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotEmpty());

            RuleFor(x => x.UnitInStock)
                .NotEmpty()
                .WithMessage(ValidationMessage.NotNull());
        }
    }
}
