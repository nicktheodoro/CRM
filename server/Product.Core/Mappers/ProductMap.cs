using AutoMapper;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using Product.Core.Contracts.Commands.Product;
using Product.Core.Contracts.Queries.Product;
using Product.Core.Models.Product;

namespace Product.Core.Mappers
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            DomainToResponse();
            CommandToDomain();
        }

        private void DomainToResponse()
        {
            CreateMap<ProductModel, GetProductResponse>();
            CreateMap<PaginationResponse<ProductModel>, PaginateQueryResponseBase<GetProductResponse>>();
        }

        private void CommandToDomain()
        {
            CreateMap<InsertProductCommand, ProductModel>();
            CreateMap<UpdateProductCommand, ProductModel>();
        }
    }
}
