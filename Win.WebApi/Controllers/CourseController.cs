using Application.Courses.Commands.CreateCommands;
using Application.Courses.Commands.UpdateCommands;
using Application.Courses.Queries.GetCourseDetails;
using Application.Courses.Queries.GetCourseList;
using Application.Videos.Commands.CreateCommands;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<CourseResponse> Create(int categoryId, string title, string description, int mentorId, IFormFile formFile)
        {
            var command = new CreateCourseCommand
            {
                CategoryId = categoryId,
                Title = title,
                Description = description,
                MentorId = mentorId,
                ImageFile = formFile
            };

            var validator = new CreateCourseCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return new CourseResponse(null, 400, "Invalid input data", false);
            }

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpPut("Update")]
        [Authorize(Roles = "2")]
        public async Task<CourseResponse> Update(int courseId, string title, string description, int mentorId, IFormFile formFile)
        {
            var command = new UpdateCourseCommand
            {
                CourseId = courseId,
                Title = title,
                Description = description,
                MentorId = mentorId,
                ImageFile = formFile
            };

            var validator = new UpdateCourseCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return new CourseResponse(null, 400, "Invalid input data", false);
            }

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpGet("getCourses/{mentorId}")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<CourseListVm>> GetCourses(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid mentor ID.")]
                      int mentorId)
        {
            var query = new GetCourseMentorQuery { MentorId = mentorId };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}/getCourseDetails")]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult<CourseDetailsVm>> GetCourseDetails(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid course ID.")]int id)
        {
            var query = new GetCourseDetailsQuery { Id = id};
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("getAllCourses")]
        [Authorize]
        public async Task<ActionResult<CourseListVm>> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var courseList = await _mediator.Send(query);

            var response = new Response(200, "Courses retrieved successfully", true);
            return Ok(new { Response = response, Courses = courseList });
        }

        [HttpGet("getStudentCourses/{studentId}")]
        [Authorize]
        public async Task<ActionResult<StudentCourseListVm>> GetStudentCourses(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid course ID.")] int studentId)
        {
            var query = new GetStudentCoursesQuery { StudentId = studentId };
            var courseList = await _mediator.Send(query);

            var response = new Response(200, "Student courses retrieved successfully", true);
            return Ok(new { Response = response, Courses = courseList.Courses });
        }

    }
}
