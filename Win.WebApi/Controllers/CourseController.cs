using Application.Courses.Commands.CreateCommands;
using Application.Courses.Queries.GetCourseDetails;
using Application.Courses.Queries.GetCourseList;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "2")]
        public async Task<Response> Create(CreateCourseCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new Response(400, "Invalid input data", false);
            }
            var response = await _mediator.Send(request);

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
        [Authorize(Roles = "2")]
        public async Task<ActionResult<CourseDetailsVm>> GetCourseDetails(int id, int mentorId)
        {
            var query = new GetCourseDetailsQuery { Id = id, MentorId = mentorId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("getAllCourses")]
        public async Task<ActionResult<CourseListVm>> GetAllCourses()
        {
            var query = new GetCourseListQuery();
            var courseList = await _mediator.Send(query);
            
            var response = new Response(200, "Courses retrieved successfully", true);
            return Ok(new { Response = response, Courses = courseList });
        }

        [HttpGet("getStudentCourses/{studentId}")]
        public async Task<ActionResult<StudentCourseListVm>> GetStudentCourses(int studentId)
        {
            var query = new GetStudentCoursesQuery { StudentId = studentId };
            var courseList = await _mediator.Send(query);

            var response = new Response(200, "Student courses retrieved successfully", true);
            return Ok(new { Response = response, Courses = courseList.Courses });
        }

    }
}
