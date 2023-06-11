using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CqrsFramework.Application.Features.User.Commands;
using CqrsFramework.Application.Features.User.Models;
using CqrsFramework.Application.Features.User.Queries;
using CqrsFramework.Application.Features.Auth.Models;
using CqrsFramework.Application.Features.Auth.Queries;
using CqrsFramework.Infrastructure.Results;

namespace CqrsFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<LoginResponse>))]
        public async Task<IActionResult> PostAsync([FromBody] GetLoginQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
