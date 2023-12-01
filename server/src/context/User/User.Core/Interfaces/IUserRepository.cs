using MyApp.SharedDomain.Interfaces;
using User.Core.Models.User;

namespace MyApp.Core.Users.Interfaces
{
    public interface IUserRepository : IEFRepository<UserModel>
    {
    }
}
