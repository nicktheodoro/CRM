using MyApp.SharedDomain.Queries;

namespace MyApp.Core.Users.Queries
{
    public class GetUserResponse : QueryResponseBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
