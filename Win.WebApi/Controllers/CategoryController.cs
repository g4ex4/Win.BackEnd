using Application.Categories.Commands.Commands;
using Application.Categories.Commands.Queries;
using Application.Courses.Queries.GetCourseList;
using Azure;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryCommand command)
        {
            if (command.Name == null)
            {
                return new CategoryResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(command);
            return (CategoryResponse)response;
            
        }

        [HttpGet("categories")]
        [Authorize]
        public async Task<ActionResult<CategoryListVm>> GetAllCategories()
        {
            var query = new GetAllCategoryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getCoursesByCategory/{categoryId}")]
        [Authorize]
        public async Task<ActionResult<CategoryListVm>> GetCoursesByCategory(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid category ID.")]
              int categoryId)
        {
            var query = new GetCourseCategoryQuery { CategoryId = categoryId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }


    }
}
