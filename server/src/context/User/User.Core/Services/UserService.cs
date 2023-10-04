using AutoMapper;
using MyApp.Core.Users.Commands;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Services;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
        {
        }

        public async Task<CommandResponse> InsertAsync(InsertUserCommand command)
        {
            var entity = _mapper.Map<User>(command);
            entity.HashPassword();

            if (!entity.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_ENTITY, validationResult);
            }

            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Successfully inserted." };
        }

        public async Task<CommandResponse> UpdateAsync(UpdateUserCommand command)
        {
            var entity = await base.GetEntityByIdAsync(command.Id);
            entity.HashPassword();

            if (!entity.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_ENTITY, validationResult);
            }

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Successfully updated." };
        }
    }
}
