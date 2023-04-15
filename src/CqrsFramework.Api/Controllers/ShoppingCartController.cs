using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CqrsFramework.Application.Features.ShoppingCart.Commands;
using CqrsFramework.Application.Features.ShoppingCart.Models;
using CqrsFramework.Application.Features.ShoppingCart.Queries;
using CqrsFramework.Infrastructure.Results;
using System.Data;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CqrsFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShoppingCartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<ShoppingCartResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetShoppingCartQuery request)
        {
            request.UserId = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(201, Type = typeof(IRequestResult))]
        public async Task<IActionResult> PostAsync([FromBody] ShoppingCartCreateDto request)
        {
            var command = _mapper.Map<CreateShoppingCartCommand>(request);
            command.UserId = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200, Type = typeof(IRequestResult))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteShoppingCartCommand(id));
            return Ok(res);
        }
    }
}
