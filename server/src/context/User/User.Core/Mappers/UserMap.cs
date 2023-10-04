using AutoMapper;
using MyApp.Core.Users.Commands;
using MyApp.Core.Users.Queries;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Mappers
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            ModelToResponse();
            CommandToModel();
        }

        private void ModelToResponse()
        {
            CreateMap<User, GetUserResponse>();
            CreateMap<PaginationResponse<User>, PaginateQueryResponseBase<GetUserResponse>>();
        }

        private void CommandToModel()
        {
            CreateMap<InsertUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(x => x.Password));

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(x => x.Password));
        }
    }
}
