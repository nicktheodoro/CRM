using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Controllers;
using Product.Core.Contracts.Commands.Product;
using Product.Core.Contracts.Queries.Product;
using Product.Core.Models.Product;

namespace CRM.Application.Controllers
{

    [ApiController]
    [Route("api/products")]
    public class ProductController(IMediator mediator) : MediatRControllerBase<
    ProductModel,
    GetProductsPaginateQuery,
    GetProductsPaginateResponse,
    GetProductQuery,
    GetProductResponse,
    InsertProductCommand,
    UpdateProductCommand,
    DeleteProductCommand>(mediator)
    {
    }
}
