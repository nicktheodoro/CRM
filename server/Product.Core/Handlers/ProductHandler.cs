using MediatR;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Handlers;
using MyApp.SharedDomain.Queries;
using Product.Core.Contracts.Commands.Product;
using Product.Core.Contracts.Queries.Product;
using Product.Core.Models.Product;
using Product.Core.Services;

namespace Product.Core.Handlers
{
    public class ProductHandler(ProductService service) :
                HandlerBase<
            ProductModel,
            GetProductQuery,
            GetProductResponse,
            GetProductsPaginateQuery,
            InsertProductCommand,
            UpdateProductCommand,
            DeleteProductCommand>(service),
        IRequestHandler<GetProductQuery, GetProductResponse>,
        IRequestHandler<GetProductsPaginateQuery, PaginateQueryResponseBase<GetProductResponse>>,
        IRequestHandler<InsertProductCommand, CommandResponse>,
        IRequestHandler<UpdateProductCommand, CommandResponse>,
        IRequestHandler<DeleteProductCommand, CommandResponse>
    {
    }
}
