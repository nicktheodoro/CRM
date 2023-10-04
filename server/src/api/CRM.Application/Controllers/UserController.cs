using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Users.Commands;
using MyApp.Core.Users.Queries;
using MyApp.SharedDomain.Commands;
using MyApp.Users.Models;

namespace MyApp.Application.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : MediatRControllerBase<
        User,
        GetUsersPaginateQuery,
        GetUsersPaginateResponse,
        GetUserQuery,
        GetUserResponse,
        InsertUserCommand,
        UpdateUserCommand,
        DeleteUserCommand>
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPatch("inactive")]
        [Authorize]
        public async Task<IActionResult> InactiveUser(InactiveUserCommand request)
        {
            return await Result(request, HttpStatusCode.OK);
        }

        [HttpPatch("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdateUserPassword request)
        {
            return await Result(request, HttpStatusCode.OK);
        }
    }
}
