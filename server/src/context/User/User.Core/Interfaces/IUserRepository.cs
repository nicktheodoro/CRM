using MyApp.SharedDomain.Interfaces;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Interfaces
{
    public interface IUserRepository : IEFRepository<User>
    {
    }
}
