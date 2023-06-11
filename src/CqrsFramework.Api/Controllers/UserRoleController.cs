using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CqrsFramework.Application.Features.Product.Commands;
using CqrsFramework.Application.Features.User.Commands;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Application.Features.User.Queries;
using System.Data;
using CqrsFramework.Application.Features.UserRole.Command;
using CqrsFramework.Application.Features.UserRole.Queries;
using CqrsFramework.Application.Features.UserRole.Models;
using CqrsFramework.Infrastructure.Results;

namespace CqrsFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<UserRoleResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetUserRoleQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost("getnotuserrole")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<UserRoleResponse>>))]
        public async Task<IActionResult> GetAsync([FromBody] GetNotUserRoleQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(IRequestResult))]
        public async Task<IActionResult> PostAsync([FromBody] CreateUserRoleCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(IRequestResult))]
        public async Task<IActionResult> DeleteAsync(DeleteUserRoleCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
