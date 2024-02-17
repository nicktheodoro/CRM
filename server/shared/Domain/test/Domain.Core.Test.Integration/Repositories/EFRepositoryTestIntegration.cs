using Bogus;
using Domain.Core.Test.Integration.Abstractions;
using Domain.Core.Test.Integration.Stubs.Contexts;
using Domain.Core.Test.Integration.Stubs.Models;
using Domain.Core.Test.Integration.Stubs.Repositories;
using Domain.Core.Test.Integration.Stubs.Repositories.Interfaces;
using FluentAssertions;
using MyApp.SharedDomain.ValueObjects;

namespace Domain.Core.Test.Integration.Repositories
{
    public class EFRepositoryTestIntegration : EFTestIntegration<ProductStubContext>
    {
        private readonly IProductStubEFRepository _repository;
        private readonly Faker _faker;

        public EFRepositoryTestIntegration()
        {
            _repository = new ProductStubEFRepository(_context);
            _faker = new Faker();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnEntity_WhenExists()
        {
            var stub = new ProductStubModel()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };
            await _repository.InsertAsync(stub);
            await _repository.SaveChangesAsync();

            var result = await _repository.GetAsync(stub.Id);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenNotFound()
        {
            var id = Guid.NewGuid();

            var result = await _repository.GetAsync(id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnPagedResults()
        {
            const int page = 1;
            const int pageSize = 2;
            for (int i = 0; i < 4; i++)
            {
                var stub = new ProductStubModel()
                {
                    Name = _faker.Commerce.ProductName(),
                    Description = _faker.Commerce.ProductDescription(),
                    UnitPrice = _faker.Random.Number(1, 100),
                    UnitInStock = _faker.Random.Number(1, 100)
                };
                await _repository.InsertAsync(stub);
            }
            await _repository.SaveChangesAsync();

            var pagination = new Pagination(page, pageSize);
            var results = await _repository.GetAllAsync(pagination);

            results.Page.Should().Be(page);
            results.PageSize.Should().Be(pageSize);
            results.Items.Should().HaveCountGreaterThan(0);
            results.Count.Should().Be(2);
        }

        [Fact]
        public async Task InsertAsync_ShouldInsertEntity()
        {
            var stub = new ProductStubModel()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };

            await _repository.InsertAsync(stub);
            await _repository.SaveChangesAsync();

            var result = await _repository.GetAsync(stub.Id);
            result?.Id.Should().NotBeEmpty();
            result?.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            var stub = new ProductStubModel()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };
            await _repository.InsertAsync(stub);
            await _repository.SaveChangesAsync();

            stub.Name = "Updated Name";

            await _repository.UpdateAsync(stub);
            await _repository.SaveChangesAsync();

            var result = await _repository.GetAsync(stub.Id);
            result?.Id.Should().Be(stub.Id);
            result?.Name.Should().BeEquivalentTo("Updated Name");
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity()
        {
            var stub = new ProductStubModel()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                UnitPrice = _faker.Random.Number(1, 100),
                UnitInStock = _faker.Random.Number(1, 100)
            };
            await _repository.InsertAsync(stub);
            await _repository.SaveChangesAsync();

            await _repository.Delete(stub);
            await _repository.SaveChangesAsync();

            var result = await _repository.GetAsync(stub.Id);
            result.Should().BeNull();
        }
    }
}
