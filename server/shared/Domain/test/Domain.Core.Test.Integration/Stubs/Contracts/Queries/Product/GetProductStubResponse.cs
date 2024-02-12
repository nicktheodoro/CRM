using MyApp.SharedDomain.Queries;

namespace Domain.Core.Test.Integration.Stubs.Contracts.Queries.Product
{
    public class GetProductStubResponse : QueryResponseBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}
