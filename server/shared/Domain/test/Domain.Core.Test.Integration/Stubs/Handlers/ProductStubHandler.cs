using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product;
using Domain.Core.Test.Integration.Stubs.Contracts.Queries.Product;
using Domain.Core.Test.Integration.Stubs.Models;
using Domain.Core.Test.Integration.Stubs.Services;
using MediatR;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Handlers;
using MyApp.SharedDomain.Queries;

namespace Domain.Core.Test.Integration.Stubs.Handlers
{
    public class ProductStubHandler(ProductStubService service) :
               HandlerBase<
           ProductStubModel,
           GetProductStubQuery,
           GetProductStubResponse,
           GetProductsStubPaginateQuery,
           InsertProductStubCommand,
           UpdateProductStubCommand,
           DeleteProductStubCommand>(service),
       IRequestHandler<GetProductStubQuery, GetProductStubResponse>,
       IRequestHandler<GetProductsStubPaginateQuery, PaginateQueryResponseBase<GetProductStubResponse>>,
       IRequestHandler<InsertProductStubCommand, CommandResponse>,
       IRequestHandler<UpdateProductStubCommand, CommandResponse>,
       IRequestHandler<DeleteProductStubCommand, CommandResponse>
    {
    }
}
