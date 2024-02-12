using MyApp.SharedDomain.Queries;

namespace Domain.Core.Test.Integration.Stubs.Contracts.Queries.Product
{
    public class GetProductsStubPaginateResponse(IEnumerable<GetProductStubResponse> items) : PaginateQueryResponseBase<GetProductStubResponse>(items)
    {
    }
}
