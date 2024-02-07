using AutoMapper;
using MyApp.SharedDomain.Services;
using Product.Core.Interfaces;
using Product.Core.Models.Product;

namespace Product.Core.Services
{

    public class ProductService(IMapper mapper, IProductRepository repository) : BaseService<ProductModel>(mapper, repository)
    {
    }
}
