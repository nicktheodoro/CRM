using FluentValidation;
using MyApp.SharedDomain.Messages;

namespace Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product.Validators
{
    public class DeleteProductStubCommandValidator : AbstractValidator<DeleteProductStubCommand>
    {
        public DeleteProductStubCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage(ValidationMessage.Required());
        }
    }
}
