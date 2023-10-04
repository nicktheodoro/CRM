using MyApp.SharedDomain.Queries;

namespace MyApp.Core.Users.Queries
{
    public class GetUsersPaginateResponse : PaginateQueryResponseBase<GetUserResponse>
    {
        public GetUsersPaginateResponse(IEnumerable<GetUserResponse> items) : base(items)
        {
        }
    }
}
