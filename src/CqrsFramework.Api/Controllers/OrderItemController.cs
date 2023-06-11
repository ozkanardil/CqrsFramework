using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CqrsFramework.Application.Features.OrderItem.Models;
using CqrsFramework.Application.Features.OrderItem.Queries;
using CqrsFramework.Infrastructure.Results;

namespace CqrsFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<OrderItemResponse>>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetOrderItemQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
