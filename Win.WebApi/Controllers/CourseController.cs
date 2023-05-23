using Application.Courses.Commands.CreateCommands;
using Application.Empl.Commands.CreateCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly SqlServerContext _context;
        private readonly IMediator _mediator;


        public CourseController(SqlServerContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<Response> Create(CreateCourseCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new EmployeeResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);
            //_emailService.SendEmailAsync(request.Email);

            return response;
        }
    }
}
