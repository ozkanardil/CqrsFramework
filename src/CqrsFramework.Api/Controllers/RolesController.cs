using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CqrsFramework.Application.Features.Role.Models;
using CqrsFramework.Application.Features.Role.Queries;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Application.Features.User.Queries;
using CqrsFramework.Infrastructure.Results;
using System.Data;

namespace CqrsFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<RoleResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetRoleQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
