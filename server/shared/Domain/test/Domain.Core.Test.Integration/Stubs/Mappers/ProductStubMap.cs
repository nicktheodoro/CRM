using AutoMapper;
using Domain.Core.Test.Integration.Stubs.Contracts.Commands.Product;
using Domain.Core.Test.Integration.Stubs.Contracts.Queries.Product;
using Domain.Core.Test.Integration.Stubs.Models;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;

namespace Domain.Core.Test.Integration.Stubs.Mappers
{
    public class ProductStubMap : Profile
    {
        public ProductStubMap()
        {
            DomainToResponse();
            CommandToDomain();
        }

        private void DomainToResponse()
        {
            CreateMap<ProductStubModel, GetProductStubResponse>();
            CreateMap<PaginationResponse<ProductStubModel>, PaginateQueryResponseBase<GetProductStubResponse>>();
        }

        private void CommandToDomain()
        {
            CreateMap<InsertProductStubCommand, ProductStubModel>();
            CreateMap<UpdateProductStubCommand, ProductStubModel>();
        }
    }
}
