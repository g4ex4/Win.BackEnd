using Application.Categories.Commands.CreateCommands;
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

        [HttpPost("addCategory")]
        public async Task<Response> AddCategory([FromBody] CreateCategoryCommand command, [FromServices] IMediator mediator)
        {
            var response = await _mediator.Send(command);

            return response;
        }
    }
}
