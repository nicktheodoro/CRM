using MyApp.SharedDomain.Queries;

namespace Product.Core.Contracts.Queries.Product
{
    public class GetProductResponse : QueryResponseBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}
