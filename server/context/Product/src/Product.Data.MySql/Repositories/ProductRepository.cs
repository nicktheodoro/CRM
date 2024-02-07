using MyApp.SharedDomain.Repositories;
using Product.Core.Interfaces;
using Product.Core.Models.Product;
using Product.Data.MySql.Contexts;

namespace Product.Data.MySql.Repositories
{
    public class ProductRepository(ProductContext context) : EFRepository<ProductModel>(context), IProductRepository
    {
    }
}
