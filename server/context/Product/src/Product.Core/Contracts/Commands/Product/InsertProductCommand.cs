using FluentValidation.Results;
using MyApp.SharedDomain.Commands;
using Product.Core.Contracts.Commands.Product.Validators;

namespace Product.Core.Contracts.Commands.Product
{
    public class InsertProductCommand : InsertCommandBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new InsertProductCommandValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
