using MyApp.SharedDomain.Queries;
using User.Core.Models.User.Image;

namespace User.Core.Contracts.Queries
{
    public class GetUserResponse : QueryResponseBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public virtual ImageModel? Image { get; set; }
    }
}
