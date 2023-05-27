using Application.Courses.Commands.CreateCommands;
using Application.Courses.Queries.GetCourseDetails;
using Application.Courses.Queries.GetCourseList;
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

        [HttpGet]
        public async Task<ActionResult<CourseListVm>> GetCourses(int mentorId)
        {
            var query = new GetCourseListQuery { MentorId = mentorId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}getCourseDetails")]
        public async Task<ActionResult<CourseDetailsVm>> GetCourseDetails(int id, int mentorId)
        {
            var query = new GetCourseDetailsQuery { Id = id, MentorId = mentorId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }


        [HttpGet("getAllCourses")]
        public async Task<ActionResult<CourseListVm>> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var courseList = await _mediator.Send(query);

            var response = new Response(200, "Courses retrieved successfully", true);
            return Ok(new { Response = response, Courses = courseList });
        }

    }
}
