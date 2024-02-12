using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyApp.Core.Users.Interfaces;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Services;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries;
using User.Core.Models.User;
using User.Core.Models.User.Image;

namespace MyApp.Core.Users.Services
{
    public class UserService(IMapper mapper, IUserRepository repository) : BaseService<UserModel>(mapper, repository)
    {
        public async Task<GetUserResponse> GetAsync(GetUserQuery query)
        {
            var entity = await GetEntityAsync(query.Id);
            return _mapper.Map<GetUserResponse>(entity);
        }

        public async Task<CommandResponse> InactiveUserAsync(InactiveUserCommand command)
        {
            var entity = await GetEntityAsync(command.Id);
            entity.InactiveUser();

            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "User Inactived" };
        }

        public async Task<CommandResponse> InsertAsync(InsertUserCommand command)
        {
            var entity = _mapper.Map<UserModel>(command);

            if (command.Image != null)
            {
                var imageEntity = await ConvertFormFileToImageModelAsync(command.Image.Content);
                entity.AddImage(imageEntity);
            }

            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Successfully created" };
        }

        public async Task<CommandResponse> UpdatePasswordAsync(UpdateUserPassword command)
        {
            var entity = await GetEntityAsync(command.Id);
            entity.UpdatePassword(command.ActualPassword, command.NewPassword);

            await _repository.SaveChangesAsync();

            return new CommandResponse() { Id = entity.Id, Message = "Password updated" };
        }

        private static async Task<ImageModel> ConvertFormFileToImageModelAsync(IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            return new ImageModel
            {
                Content = memoryStream.ToArray(),
                ContentType = formFile.ContentType
            };
        }
    }
}
