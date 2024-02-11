using Domain.Core.Test.Integration.Stubs.Models;
using Microsoft.EntityFrameworkCore;
using MyApp.SharedDomain.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Core.Test.Integration.Stubs.Contexts
{
    public class ProductStubContext : EFContext
    {
        [ExcludeFromCodeCoverage]
        public ProductStubContext() : base()
        {
        }

        public ProductStubContext(DbContextOptions<ProductStubContext> options) : base(options)
        {
        }

        public DbSet<ProductStub> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
