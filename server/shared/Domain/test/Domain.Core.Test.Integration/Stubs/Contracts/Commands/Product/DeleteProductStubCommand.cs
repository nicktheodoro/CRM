using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product.Validators;
using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product
{
    public class DeleteProductStubCommand : CommandBase
    {
        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new DeleteProductStubCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
