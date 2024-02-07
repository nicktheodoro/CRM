using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.Core.Handlers;
using Product.Core.Interfaces;
using Product.Core.Mappers;
using Product.Core.Services;
using Product.Data.MySql.Contexts;
using Product.Data.MySql.Repositories;

namespace Product.CrossCutting.Ioc.DependencyInjection
{
    public class ProductNativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, string? connection)
        {
            services.AddAutoMapper(typeof(ProductMap));
            services.AddDbContext<ProductContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("Product.Core")));
            services.AddScoped<ProductHandler>();
            services.AddScoped<ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ProductHandler).Assembly));
        }
    }
}
