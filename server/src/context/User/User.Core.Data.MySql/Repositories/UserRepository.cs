using Microsoft.EntityFrameworkCore;
using MyApp.Core.Users.Data.MySql.Contexts;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Repositories;
using User.Core.Models.User;

namespace MyApp.Core.Users.Data.MySql.Repositories
{
    public class UserRepository : EFRepository<UserModel>, IUserRepository
    {
        public UserRepository(UserContext context) : base(context)
        {
        }

        public async override Task<UserModel?> GetByIdAsync(Guid id)
        {
            // TODO: Implements commandTimeout on EFRepository.
            //_dbContext.Database.SetCommandTimeout(commandTImeout);

            return await _dbSet
                               .Include(x => x.Image)
                               .Where(x => x.Id == id)
                               .FirstOrDefaultAsync();
        }
    }
}
