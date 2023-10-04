using MediatR;
using MyApp.Core.Users.Commands;
using MyApp.Core.Users.Queries;
using MyApp.Core.Users.Services;
using MyApp.SharedDomain.Commands;
using MyApp.SharedDomain.Exceptions.ValidacaoException;
using MyApp.SharedDomain.Handlers;
using MyApp.SharedDomain.Queries;
using MyApp.Users.Models;

namespace MyApp.Core.Users.Handlers
{
    public class UserHandler :
        HandlerBase<
            User,
            GetUserQuery,
            GetUserResponse,
            GetUsersPaginateQuery,
            InsertUserCommand,
            UpdateUserCommand,
            DeleteUserCommand>,
        IRequestHandler<GetUserQuery, GetUserResponse>,
        IRequestHandler<GetUsersPaginateQuery, PaginateQueryResponseBase<GetUserResponse>>,
        IRequestHandler<InsertUserCommand, CommandResponse>,
        IRequestHandler<UpdateUserCommand, CommandResponse>,
        IRequestHandler<DeleteUserCommand, CommandResponse>,
        IRequestHandler<InactiveUserCommand, CommandResponse>,
        IRequestHandler<UpdateUserPassword, CommandResponse>
    {
        private readonly DomainService _domainService;
        private readonly UserService _userService;

        public UserHandler(UserService userService, DomainService domainService) : base(userService)
        {
            _domainService = domainService;
            _userService = userService;
        }

        public async Task<CommandResponse> Handle(InactiveUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_QUERY, validationResult);
            }

            return await _domainService.InactiveUser(request);
        }

        public async Task<CommandResponse> Handle(UpdateUserPassword request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_QUERY, validationResult);
            }

            return await _domainService.UpdatePassword(request);
        }

        public async override Task<CommandResponse> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _userService.InsertAsync(request);
        }

        public async override Task<CommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Valid(out var validationResult))
            {
                throw new ValidacaoException(INVALID_COMMAND, validationResult);
            }

            return await _userService.UpdateAsync(request);
        }
    }
}
