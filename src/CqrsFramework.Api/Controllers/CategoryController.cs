using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using CqrsFramework.Application.Features.Category.Commands;
using CqrsFramework.Application.Features.Category.Queries;
using CqrsFramework.Application.Features.Category.Models;

namespace CqrsFramework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryResponse>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetCategoryQuery request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(CategoryResponse))]
        public async Task<IActionResult> PostAsync([FromBody] CreateCategoryCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(CategoryResponse))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateCategoryCommand request)
        {
            var res = await _mediator.Send(request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(CategoryResponse))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var res = await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(res);
        }
    }
}
