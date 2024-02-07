using MyApp.SharedDomain.Interfaces;
using Product.Core.Models.Product;

namespace Product.Core.Interfaces
{
    public interface IProductRepository : IEFRepository<ProductModel>
    {
    }
}
