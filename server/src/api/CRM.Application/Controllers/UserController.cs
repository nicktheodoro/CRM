using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using User.Core.Contracts.Commands;
using User.Core.Contracts.Queries;
using User.Core.Contracts.Queries.User.Image;
using User.Core.Models.User;

namespace MyApp.Application.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : MediatRControllerBase<
        UserModel,
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

        [HttpPost]
        public override Task<IActionResult> Insert([FromForm] InsertUserCommand request)
        {
            return base.Insert(request);
        }

        [HttpPatch("inactive")]
        [Authorize]
        public async Task<IActionResult> InactiveUser(InactiveUserCommand request)
        {
            return await Result(request, HttpStatusCode.OK);
        }

        [HttpGet("{id}/image")]
        [Authorize]
        public async Task<IActionResult> GetUserImage(Guid id)
        {
            return await Result(new GetUserImageQuery() { Id = id }, HttpStatusCode.OK);
        }

        [HttpPatch("update-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdateUserPassword request)
        {
            return await Result(request, HttpStatusCode.OK);
        }
    }
}
