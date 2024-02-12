using AutoMapper;
using Domain.Core.Messages;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Interfaces;
using MyApp.SharedDomain.Messages;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;
using System.Net;

namespace MyApp.SharedDomain.Services
{
    public class BaseService<TEntity>(IMapper mapper, IEFRepository<TEntity> repository) where TEntity : Entity
    {
        protected readonly IMapper _mapper = mapper;
        protected readonly IEFRepository<TEntity> _repository = repository;

        public virtual async Task<TEntity> GetEntityAsync(Guid id)
        {
            return await _repository.GetAsync(id) ?? throw new NotFoundException(id);
        }

        public virtual async Task<TResponse> GetAsync<TResponse>(QueryBase<TResponse> query)
        {
            var entity = await GetEntityAsync(query.Id);

            var response = _mapper.Map<TResponse>(entity)
                ?? throw new ExceptionBase(ErrorMessage.UnprocessableContent(), HttpStatusCode.UnprocessableContent);

            return response;
        }

        public virtual async Task<PaginateQueryResponseBase<TResponse>> GetAllAsync<TResponse>(PaginateQueryBase<TResponse> paginateQuery)
        {
            var pagination = new Pagination(paginateQuery.Page, paginateQuery.PageSize);
            var entities = await _repository.GetAllAsync(pagination);

            var response = _mapper.Map<PaginateQueryResponseBase<TResponse>>(entities)
                ?? throw new ExceptionBase(ErrorMessage.UnprocessableContent(), HttpStatusCode.UnprocessableContent);

            return response;
        }

        public virtual async Task<CommandResponse> InsertAsync(InsertCommandBase command)
        {
            var entity = _mapper.Map<TEntity>(command)
                ?? throw new ExceptionBase(ErrorMessage.UnprocessableContent(), HttpStatusCode.UnprocessableContent);

            if (!entity.Valid(out var validationResult))
            {
                throw new ValidacaoException(ValidationMessage.InvalidEntity(), validationResult);
            }

            await _repository.InsertAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse { Id = entity.Id, Message = "Successfully created" };
        }

        public virtual async Task<CommandResponse> UpdateAsync(CommandBase command)
        {
            var entity = _mapper.Map<TEntity>(command)
                ?? throw new ExceptionBase(ErrorMessage.UnprocessableContent(), HttpStatusCode.UnprocessableContent);

            if (!entity.Valid(out var validationResult))
            {
                throw new ValidacaoException(ValidationMessage.InvalidEntity(), validationResult);
            }

            await _repository.UpdateAsync(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse { Id = entity.Id, Message = "Successfully updated" };
        }

        public virtual async Task<CommandResponse> DeleteAsync(CommandBase command)
        {
            var entity = await GetEntityAsync(command.Id);

            await _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            return new CommandResponse { Id = entity.Id, Message = "Successfully deleted" };
        }
    }
}
