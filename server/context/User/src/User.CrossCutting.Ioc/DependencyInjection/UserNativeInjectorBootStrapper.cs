using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Core.Users.Data.MySql.Contexts;
using MyApp.Core.Users.Data.MySql.Repositories;
using MyApp.Core.Users.Handlers;
using MyApp.Core.Users.Interfaces;
using MyApp.Core.Users.Mappers;
using MyApp.Core.Users.Services;

namespace User.CrossCutting.Ioc.DependencyInjection
{
    public class UserNativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, string? connection)
        {
            services.AddAutoMapper(typeof(UserMap));
            services.AddDbContext<UserContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("User.Core")));
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(UserHandler).Assembly));
            services.AddScoped<UserHandler>();
            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
