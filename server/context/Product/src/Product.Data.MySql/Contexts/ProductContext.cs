using Microsoft.EntityFrameworkCore;
using MyApp.SharedDomain.Repositories;
using Product.Core.Models.Product;

namespace Product.Data.MySql.Contexts
{

    public class ProductContext : EFContext
    {
        public ProductContext() : base() { }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
