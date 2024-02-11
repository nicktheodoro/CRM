using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Repositories;
using System.Net;

namespace Domain.Core.Test.Integration.Abstractions
{
    public abstract class EFTestIntegration<T> where T : EFContext, new()
    {
        protected DbContextOptionsBuilder<T> dbBuilder;
        protected T context;

        public EFTestIntegration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            dbBuilder = new DbContextOptionsBuilder<T>().UseNpgsql(connectionString);

            context = Activator.CreateInstance(typeof(T), dbBuilder.Options) as T
                ?? throw new ExceptionBase($"Failed to create test context.", HttpStatusCode.InternalServerError);
        }
    }
}
