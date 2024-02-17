using AutoMapper;
using Domain.Core.Test.Integration.Stubs.Models;
using Domain.Core.Test.Integration.Stubs.Repositories;
using MyApp.SharedDomain.Services;

namespace Domain.Core.Test.Integration.Stubs.Services
{
    public class ProductStubService(IMapper mapper, ProductStubEFRepository repository) : BaseService<ProductStubModel>(mapper, repository)
    {
    }
}
