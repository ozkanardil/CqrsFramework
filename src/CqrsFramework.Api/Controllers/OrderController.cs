using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using CqrsFramework.Application.Features.Order.Models;
using CqrsFramework.Application.Features.Order.Commands;
using CqrsFramework.Application.Features.Order.Queries;
using CqrsFramework.Infrastructure.Results;

namespace CqrsFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(200, Type = typeof(IRequestDataResult<IEnumerable<OrderResponse>>))]
        public async Task<IActionResult> GetAsync()
        {
            GetOrderQuery request = new GetOrderQuery();
            request.UserId = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(201, Type = typeof(IRequestResult))]
        public async Task<IActionResult> PostAsync(OrderCreateDto request)
        {
            CreateOrderCommand model = _mapper.Map<CreateOrderCommand>(request);
            model.UserId = Convert.ToInt32(User.Claims.Where(a => a.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);

            var res = await _mediator.Send(model);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(200, Type = typeof(IRequestResult))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteOrderCommand(id));
            return Ok(res);
        }
    }
}
