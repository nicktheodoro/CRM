using Microsoft.EntityFrameworkCore;
using MyApp.SharedDomain.Repositories;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Data.MySql.Contexts
{
    public class UserContext : EFContext
    {
        public UserContext() : base() { }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
