using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product.Validators;
using FluentValidation.Results;
using MyApp.SharedDomain.Commands;

namespace Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product
{
    public class UpdateProductStubCommand : CommandBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new UpdateProductStubCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
