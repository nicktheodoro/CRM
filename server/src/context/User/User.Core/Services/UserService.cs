using AutoMapper;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Services;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries.User.Image;
using User.Core.Models.User;

namespace MyApp.Core.Users.Services
{
    public class UserService : BaseService<UserModel>
    {
        public UserService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
        {
        }

        public async Task<CommandResponse> InactiveUserAsync(InactiveUserCommand command)
        {
            var entity = await GetEntityByIdAsync(command.Id);
            entity.InactiveUser();

            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "User Inactived" };
        }

        public async Task<CommandResponse> InsertAsync(InsertUserCommand command)
        {
            var entity = _mapper.Map<UserModel>(command);
            entity.AddImage(command.Image?.Content);

            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Successfully created" };
        }

        public async Task<CommandResponse> UpdatePasswordAsync(UpdateUserPassword command)
        {
            var entity = await GetEntityByIdAsync(command.Id);
            entity.UpdatePassword(command.ActualPassword, command.NewPassword);

            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Password updated" };
        }

        public async Task<GetUserImageResponse> GetUserImageAsync(GetUserImageQuery query)
        {
            var user = await GetEntityByIdAsync(query.Id);
            return new GetUserImageResponse() { 
                Id = user.Id.ToString(), 
                Content = user.Image?.Content, 
                ContentType = user.Image?.ContentType 
            };
        }
    }
}
