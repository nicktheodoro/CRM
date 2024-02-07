using FluentValidation.Results;
using MyApp.SharedDomain.ValueObjects;
using Product.Core.Models.Product.Validators;

namespace Product.Core.Models.Product
{
    public class ProductModel : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        
        //public string productCategory { get; set; }

        public override bool Valid(out ValidationResult validationResult)
        {
            validationResult = new ProductModelValidator().Validate(this);
            return validationResult.IsValid;
        }
    }
}
