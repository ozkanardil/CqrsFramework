using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CqrsFramework.Application.Features.Category.Commands;
using CqrsFramework.Application.Features.Category.Models;
using CqrsFramework.Application.Features.Category.Queries;
using CqrsFramework.Application.Features.Product.Commands;
using CqrsFramework.Application.Features.Product.Queries;
using CqrsFramework.Application.Features.Product.Models;
using CqrsFramework.Application.Features.Product.Commands;

namespace CqrsFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductResponse>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetProductQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(ProductResponse))]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(ProductResponse))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateProductCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(ProductResponse))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(res);
        }
    }
}
