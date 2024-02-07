using MyApp.SharedDomain.Queries;

namespace Product.Core.Contracts.Queries.Product
{
    public class GetProductsPaginateResponse(IEnumerable<GetProductResponse> items) : PaginateQueryResponseBase<GetProductResponse>(items)
    {
    }
}
