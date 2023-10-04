using AutoMapper;
using MyApp.Core.Users.Commands;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Services;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Services
{
    public class DomainService : BaseService<User>
    {
        public DomainService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
        {
        }

        public async Task<CommandResponse> InactiveUser(InactiveUserCommand command)
        {
            var entity = await GetEntityByIdAsync(command.Id);
            entity.InactiveUser();

            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "User Inactived." };
        }

        public async Task<CommandResponse> UpdatePassword(UpdateUserPassword command)
        {
            var entity = await GetEntityByIdAsync(command.Id);
            entity.UpdatePassword(command.ActualPassword, command.NewPassword);

            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Password updated." };
        }
    }
}
