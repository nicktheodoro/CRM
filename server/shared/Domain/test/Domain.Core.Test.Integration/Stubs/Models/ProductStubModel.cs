using Domain.Core.Test.Integration.Stubs.Models.Validators;
using FluentValidation.Results;
using MyApp.SharedDomain.ValueObjects;

namespace Domain.Core.Test.Integration.Stubs.Models
{
    public class ProductStubModel : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new ProductStubValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
