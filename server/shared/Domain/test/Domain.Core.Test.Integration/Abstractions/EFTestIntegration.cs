using AutoMapper;
using Domain.Core.Test.Integration.Stubs.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Repositories;
using System.Net;

namespace Domain.Core.Test.Integration.Abstractions
{
    public abstract class EFTestIntegration<T> where T : EFContext, new()
    {
        protected readonly T _context;
        protected readonly DbContextOptionsBuilder<T> _dbBuilder;
        protected readonly IMapper _mapper;

        public EFTestIntegration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(ProductStubMap)));
            _mapper = new Mapper(config);

            _dbBuilder = new DbContextOptionsBuilder<T>().UseNpgsql(connectionString);

            _context = Activator.CreateInstance(typeof(T), _dbBuilder.Options) as T
                ?? throw new ExceptionBase($"Failed to create test context.", HttpStatusCode.InternalServerError);
        }
    }
}
