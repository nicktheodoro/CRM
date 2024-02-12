using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product.Validators
{
    public class UpdateProductStubCommandValidator : AbstractValidator<UpdateProductStubCommand>
    {
        public UpdateProductStubCommandValidator()
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
