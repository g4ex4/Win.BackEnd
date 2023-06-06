using Application.Categories.Commands.Queries;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<CategoryListVm>> GetAllCategories()
        {
            var query = new GetAllCategoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


    }
}
