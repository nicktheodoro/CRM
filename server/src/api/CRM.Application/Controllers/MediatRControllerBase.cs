using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Queries;
using MyApp.SharedDomain.ValueObjects;

namespace MyApp.Application.Controllers
{
    public abstract class MediatRControllerBase<
        TEntity,
        TGetPaginateQuery,
        TGetPaginateResponse,
        TGetQuery,
        TGetResponse,
        TInsertCommand,
        TUpdateCommand,
        TDeleteCommand> : ControllerBase
        where TEntity : Entity
        where TGetPaginateQuery : PaginateQueryBase<TGetResponse>
        where TGetPaginateResponse : PaginateQueryResponseBase<TGetResponse>
        where TGetQuery : QueryBase<TGetResponse>
        where TGetResponse : QueryResponseBase
        where TInsertCommand : InsertCommandBase
        where TUpdateCommand : CommandBase
        where TDeleteCommand : CommandBase
    {
        public readonly IMediator _mediator;

        public MediatRControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var request = Activator.CreateInstance(typeof(TGetPaginateQuery)) as TGetPaginateQuery
                ?? throw new ExceptionBase("Invalid request type.", HttpStatusCode.InternalServerError);

            request.Page = page;
            request.PageSize = pageSize;

            return await Result(request, HttpStatusCode.OK);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = Activator.CreateInstance(typeof(TGetQuery)) as TGetQuery
                ?? throw new ExceptionBase("Invalid request type.", HttpStatusCode.InternalServerError);

            request.Id = id;

            return await Result(request, HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TInsertCommand request)
        {
            return await Result(request, HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TUpdateCommand request)
        {
            return await Result(request, HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = Activator.CreateInstance(typeof(TDeleteCommand)) as TDeleteCommand
                   ?? throw new ExceptionBase("Invalid request type.", HttpStatusCode.InternalServerError);

            request.Id = id;

            return await Result(request, HttpStatusCode.NoContent);
        }

        protected async Task<IActionResult> Result(IBaseRequest request, HttpStatusCode statusCode)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (statusCode == HttpStatusCode.NoContent)
                {
                    return NoContent();
                }

                return new ObjectResult(response)
                {
                    StatusCode = (int)statusCode
                };
            }
            catch (ValidacaoException ex)
            {
                return new ObjectResult(ex.FormatedMessage)
                {
                    StatusCode = ex.StatusCode
                };
            }
            catch (ExceptionBase ex)
            {
                return new ObjectResult(new { ex.Message })
                {
                    StatusCode = ex.StatusCode
                };
            }
        }
    }
}
