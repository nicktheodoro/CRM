using Bogus;
using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product;
using Domain.Core.Test.Integration.Stubs.Models;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.ValueObjects;

namespace Domain.Core.Test.Integration.Stubs.Fixtures
{
    public static class ProductStubFixture
    {
        private static readonly Faker _faker = new();

        public static Entity GetInvalidEntityStub<T>()
        {
            var stub = Activator.CreateInstance(typeof(T)) as Entity;

            return stub;
        }

        public static CommandBase GetInvalidCommandStub<T>()
        {
            var stub = Activator.CreateInstance(typeof(T)) as CommandBase;

            return stub;
        }

        public static InsertCommandBase GetInvalidInsertCommandStub<T>()
        {
            var stub = Activator.CreateInstance(typeof(T)) as InsertCommandBase;

            return stub;
        }

        public static ProductStub GetProductStub()
        {
            var stub = new ProductStub()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };

            return stub;
        }

        public static InsertProductStubCommand GetInsertProductStubCommand()
        {
            var stub = new InsertProductStubCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };

            return stub;
        }

        public static UpdateProductStubCommand GetUpdateProductStubCommand(ProductStub entitty)
        {
            var stub = new UpdateProductStubCommand()
            {
                Id = entitty.Id,
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };

            return stub;
        }

        public static DeleteProductStubCommand GetDeleteProductStubCommand(ProductStub entitty)
        {
            var stub = new DeleteProductStubCommand()
            {
                Id = entitty.Id,
            };

            return stub;
        }
    }
}
