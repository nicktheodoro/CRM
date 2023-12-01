using AutoMapper;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries;
using User.Core.Models.User;

namespace MyApp.Core.Users.Mappers
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            DomainToResponse();
            CommandToDomain();
        }

        private void DomainToResponse()
        {
            CreateMap<UserModel, GetUserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => x.IsActive))
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<PaginationResponse<UserModel>, PaginateQueryResponseBase<GetUserResponse>>();
        }

        private void CommandToDomain()
        {
            CreateMap<InsertUserCommand, UserModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(x => x.Password));

            //CreateMap<InsertUserCommand, ImageModel>()
            //    .ForMember(dest => dest.IM, opt => opt.Ignore());
            //.ForMember(dest => dest._content, opt => opt.MapFrom(x => x.Content));

            CreateMap<UpdateUserCommand, UserModel>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(x => x.Password));
        }
    }
}
