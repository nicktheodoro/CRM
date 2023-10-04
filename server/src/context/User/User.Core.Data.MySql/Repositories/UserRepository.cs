using MyApp.Core.Users.Data.MySql.Contexts;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Repositories;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Data.MySql.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context)
        {
        }
    }
}
