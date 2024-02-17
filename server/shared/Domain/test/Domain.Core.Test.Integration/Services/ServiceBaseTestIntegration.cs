using AutoMapper;
using Domain.Core.Test.Integration.Abstractions;
using Domain.Core.Test.Integration.Stubs.Contexts;
using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product;
using Domain.Core.Test.Integration.Stubs.Contracts.Queries.Product;
using Domain.Core.Test.Integration.Stubs.Fixtures;
using Domain.Core.Test.Integration.Stubs.Mappers;
using Domain.Core.Test.Integration.Stubs.Models;
using Domain.Core.Test.Integration.Stubs.Repositories;
using FluentAssertions;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Services;

namespace Domain.Core.Test.Integration.Services
{
    public class ServiceBaseTestIntegration : EFTestIntegration<ProductStubContext>
    {
        private readonly BaseService<ProductStubModel> _service;

        public ServiceBaseTestIntegration()
        {
            
            var repository = new ProductStubEFRepository(_context);
            _service = new BaseService<ProductStubModel>(_mapper, repository);
        }

        [Fact(DisplayName = "Dado o método GetAlAsync,  deve retornar uma coleção com sucesso")]
        public async Task Dado_Metodo_GetAllAsync_Deve_Retornar_Uma_Colecao_Com_Sucesso()
        {
            var query = new GetProductsStubPaginateQuery() { Page = 1, PageSize = 10 };
            var response = await _service.GetAllAsync(query);

            response.Should().NotBeNull();
            response.Page.Should().Be(query.Page);
            response.PageSize.Should().Be(query.PageSize);
        }

        [Fact(DisplayName = "Dado o método GetAsync, quando informado um id existente, deve retornar com sucesso")]
        public async Task Dado_Metodo_InserirAsync_Quando_Informado_Um_Id_Existente_Deve_Retornar_Com_Sucesso()
        {
            var command = ProductStubFixture.GetInsertProductStubCommand();
            var inserted = await _service.InsertAsync(command);
            var query = new GetProductStubQuery() { Id = inserted.Id };

            var response = await _service.GetAsync(query);

            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Name.Should().Be(command.Name);
            response.UnitInStock.Should().Be(command.UnitInStock);
            response.UnitPrice.Should().Be(command.UnitPrice);
        }

        [Fact(DisplayName = "Dado o método InserirAsync, quando informado um command inválido, deve retornar erro")]
        public async void Dado_Metodo_InserirAsync_Quando_Informado_Um_Command_Invalido_Deve_Retornar_Erro()
        {
            var command = ProductStubFixture.GetInvalidInsertCommandStub<InsertProductStubCommand>();

            await Assert.ThrowsAsync<ValidationException>(async () => await _service.InsertAsync(command));
        }

        [Fact(DisplayName = "Dado o método InserirAsync, quando informado um command válido, deve inserir com sucesso")]
        public async Task Dado_Metodo_InserirAsync_Quando_Informado_Um_Command_Valido_Deve_Inserir_Com_Sucesso()
        {
            var command = ProductStubFixture.GetInsertProductStubCommand();

            var response = await _service.InsertAsync(command);

            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Successfully created");
        }

        [Fact(DisplayName = "Dado o método AtualizarAsync, quando informado um command inválido, deve retornar erro")]
        public async void Dado_Metodo_AtualizarAsync_Quando_Informado_Um_Command_Invalido_Deve_Retornar_Erro()
        {
            var entidade = ProductStubFixture.GetInvalidCommandStub<UpdateProductStubCommand>();

            await Assert.ThrowsAsync<ValidationException>(async () => await _service.UpdateAsync(entidade));
        }

        [Fact(DisplayName = "Dado o método AtualizarAsync, quando informado um command válido, deve inserir com sucesso")]
        public async Task Dado_Metodo_AtualizarAsync_Quando_Informado_Um_Command_Valido_Deve_Inserir_Com_Sucesso()
        {
            var insertCommand = ProductStubFixture.GetInsertProductStubCommand();
            var inserted = await _service.InsertAsync(insertCommand);
            var entity = await _service.GetEntityAsync(inserted.Id);
            _context.ChangeTracker.Clear();

            var updateCommand = ProductStubFixture.GetUpdateProductStubCommand(entity);
            var response = await _service.UpdateAsync(updateCommand);

            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Successfully updated");
        }

        [Fact(DisplayName = "Dado o método DeleteAsync, quando informado um command válido, deve excluir com sucesso")]
        public async Task Dado_Metodo_DeleteAsync_Quando_Informada_Entidade_Existente_Deve_Excluir_Com_Sucesso()
        {
            var insertCommand = ProductStubFixture.GetInsertProductStubCommand();
            var inserted = await _service.InsertAsync(insertCommand);
            var entity = await _service.GetEntityAsync(inserted.Id);
            _context.ChangeTracker.Clear();

            var command = new DeleteProductStubCommand { Id = entity.Id };
            var response = await _service.DeleteAsync(command);

            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Successfully deleted");
        }
    }
}
