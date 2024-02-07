using FluentValidation.Results;
using MyApp.SharedDomain.Commands;
using Product.Core.Contracts.Commands.Product.Validators;

namespace Product.Core.Contracts.Commands.Product
{
    public class DeleteProductCommand : CommandBase
    {
        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new DeleteProductCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }

}
