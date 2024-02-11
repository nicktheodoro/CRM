using Domain.Core.Test.Integration.Stubs.Models;
using Domain.Core.Test.Integration.Stubs.Repositories.Interfaces;
using MyApp.SharedDomain.Repositories;

namespace Domain.Core.Test.Integration.Stubs.Repositories
{
    public class ProductStubEFRepository(EFContext context) : EFRepository<ProductStub>(context), IProductStubEFRepository
    {
    }
}
