using Domain.Core.Test.Integration.Abstractions;
using Domain.Core.Test.Integration.Stubs.Contexts;
using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product;
using Domain.Core.Test.Integration.Stubs.Contracts.Queries.Product;
using Domain.Core.Test.Integration.Stubs.Handlers;
using Domain.Core.Test.Integration.Stubs.Repositories;
using Domain.Core.Test.Integration.Stubs.Services;
using FluentAssertions;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Exceptions.ValidacaoException;

namespace Domain.Core.Test.Integration.Handlers
{
    public class HandlerBaseTestIntegration : EFTestIntegration<ProductStubContext>
    {
        private readonly ProductStubHandler _handler;
        private readonly CancellationToken _cancellationToken;

        public HandlerBaseTestIntegration()
        {
            var service = new ProductStubService(_mapper, new ProductStubEFRepository(_context));
            _handler = new ProductStubHandler(service);
            _cancellationToken = new CancellationToken();
        }

        [Fact(DisplayName = @"Given a handler, when an invalid insertion command is passed, it should throw an exception.")]
        public async void Given_A_Handler_When_An_Invalid_Insertion_Command_Is_Passed_It_Should_Throw_An_Exception()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new InsertProductStubCommand(), _cancellationToken));
            exception.Message.Should().Contain("Invalid command");

            exception.FormattedMessage.Errors.Count().Should().Be(4);

            var formattedErrors = exception.FormattedMessage.Errors.GetEnumerator();

            formattedErrors.MoveNext();
            formattedErrors.Current.PropertyName.Should().Be("Name");
            formattedErrors.Current.ErrorMessage.Should().Be("Field is required.");
        }

        [Fact(DisplayName = @"Given a handler, when an invalid query command is passed, it should throw an exception.")]
        public async void Given_A_Handler_When_An_Invalid_Query_Command_Is_Passed_It_Should_Throw_An_Exception()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new GetProductStubQuery(), _cancellationToken));
            exception.Message.Should().Contain("Invalid query");

            exception.FormattedMessage.Errors.Count().Should().Be(1);

            var formattedErrors = exception.FormattedMessage.Errors.GetEnumerator();

            formattedErrors.MoveNext();
            formattedErrors.Current.PropertyName.Should().Be("Id");
            formattedErrors.Current.ErrorMessage.Should().Be("The field cannot be empty.");
        }

        [Fact(DisplayName = @"Given a handler, when a query command for a non-existent record is passed, it should throw an exception.")]
        public async void Given_A_Handler_When_A_Query_Command_For_A_NonExistent_Record_Is_Passed_It_Should_Return_Null_Record()
        {
            var command = new GetProductStubQuery()
            {
                Id = new Guid("f6487086-d21d-4377-beee-2edd637da407")
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, _cancellationToken));
            exception.Message.Should().Contain($"The item {command.Id} was not found.");
        }

        [Fact(DisplayName = @"Given a handler, when an invalid update command is passed, it should throw an exception.")]
        public async void Given_A_Handler_When_An_Invalid_Update_Command_Is_Passed_It_Should_Throw_An_Exception()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new UpdateProductStubCommand(), _cancellationToken));
            exception.Message.Should().Contain("Invalid command");

            exception.FormattedMessage.Errors.Count().Should().Be(5);

            var formattedErrors = exception.FormattedMessage.Errors.GetEnumerator();

            formattedErrors.MoveNext();
            formattedErrors.Current.PropertyName.Should().Be("Id");
            formattedErrors.Current.ErrorMessage.Should().Be("Field is required.");
        }

        [Fact(DisplayName = @"Given a handler, when an invalid delete command is passed, it should throw an exception.")]
        public async void Given_A_Handler_When_An_Invalid_Delete_Command_Is_Passed_It_Should_Throw_An_Exception()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new DeleteProductStubCommand(), _cancellationToken));
            exception.Message.Should().Contain("Invalid command");

            exception.FormattedMessage.Errors.Count().Should().Be(1);

            var formattedErrors = exception.FormattedMessage.Errors.GetEnumerator();

            formattedErrors.MoveNext();
            formattedErrors.Current.PropertyName.Should().Be("Id");
            formattedErrors.Current.ErrorMessage.Should().Be("Field is required.");
        }

        [Fact(DisplayName = @"Given a handler, when a delete command for a non-existent record is passed, it should return a null record.")]
        public async void Given_A_Handler_When_A_Delete_Command_For_A_NonExistent_Record_Is_Passed_It_Should_Return_Null_Record()
        {
            var command = new DeleteProductStubCommand()
            {
                Id = new Guid("f6487086-d21d-4377-beee-2edd637da407")
            };

            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, _cancellationToken));
            exception.Message.Should().Contain($"The item {command.Id} was not found.");
        }

        [Fact(DisplayName = @"Given a handler, when an invalid paginated query command is passed, it should throw an exception.")]
        public async void Given_A_Handler_When_An_Invalid_Paginated_Query_Command_Is_Passed_It_Should_Throw_An_Exception()
        {
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _handler.Handle(new GetProductsStubPaginateQuery(), _cancellationToken));
            exception.Message.Should().Contain("Invalid query");

            exception.FormattedMessage.Errors.Count().Should().Be(2);

            var formattedErrors = exception.FormattedMessage.Errors.GetEnumerator();

            formattedErrors.MoveNext();
            formattedErrors.Current.PropertyName.Should().Be("Page");
            formattedErrors.Current.ErrorMessage.Should().Be("The field Page cannot be empty.");

            formattedErrors.MoveNext();
            formattedErrors.Current.PropertyName.Should().Be("PageSize");
            formattedErrors.Current.ErrorMessage.Should().Be("The field PageSize cannot be empty.");
        }
    }
}
