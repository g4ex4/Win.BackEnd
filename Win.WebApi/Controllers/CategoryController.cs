using Application.Categories.Commands.CreateCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly SqlServerContext _context;
        private readonly IMediator _mediator;


        public CategoryController(SqlServerContext context, IMediator mediator)
        {
            _context = context;
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
