using Application.Courses.Commands.DeleteCommands;
using Application.Empl.Commands.DeleteCommands;
using Application.Empl.Commands.UpdateCommands;
using Application.Empl.Queries;
using Application.Students.Queries;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "1")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AdminController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("confirmMentor")]
        public async Task<ActionResult<PersonResponse>> ConfirmMentor(ConfirmMentorCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data" });
            }

            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return NotFound(new { Message = response.Message });
            }

            return Ok(response);
        }

        [HttpPost("promoteMentorToAdmin")]
        public async Task<ActionResult<PersonResponse>> PromoteMentorToAdmin(PromoteMentorToAdminCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data" });
            }

            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return NotFound(new { Message = response.Message });
            }

            _logger.LogInformation($"Mentor with ID {command.MentorId} promoted to administrator");

            return Ok(response);
        }

        [HttpGet("getAllMentors")]
        public async Task<ActionResult<List<MentorLookupDto>>> GetAllMentors()
        {
            var query = new GetAllMentorsQuery();
            var mentorsList = await _mediator.Send(query);

            var response = new Response(200, "Mentors retrieved successfully", true);
            return Ok(new { Response = response, Mentors = mentorsList });
        }

        [HttpGet("getMentorById")]
        public async Task<ActionResult<MentorLookupDto>> GetMentorById(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid MentorId")] int id)
        {
            var query = new GetMentorByIdQuery { MentorId = id };
            var mentor = await _mediator.Send(query);

            if (mentor == null)
            {
                return NotFound(new { Message = "Mentor not found" });
            }

            return Ok(mentor);
        }

        [HttpGet("getStudentById")]
        public async Task<ActionResult<StudentLookupDto>> GetStudentById(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid StudentId")] int id)
        {
            var query = new GetStudentByIdQuery { StudentId = id };
            var student = await _mediator.Send(query);

            if (student == null)
            {
                return NotFound(new { Message = "Student not found" });
            }

            return Ok(student);
        }

        [HttpGet("getAllStudents")]
        public async Task<ActionResult<List<StudentLookupDto>>> GetAllStudents()
        {
            var query = new GetAllStudentsQuery();
            var mentorsList = await _mediator.Send(query);

            var response = new Response(200, "Students retrieved successfully", true);
            return Ok(new { Response = response, Students = mentorsList });
        }

        [HttpDelete("Delete-Mentor")]
        public async Task<Response> DeleteEmployee(DeleteEmplCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new Response(400, "Invalid input data", false);
            }

            var response = await _mediator.Send(request);

            _logger.LogInformation($"Mentor with ID {request.EmployeeId} removed");

            return response;
        }

        [HttpDelete("Delete-Student")]
        public async Task<Response> DeleteStudent(DeleteStudentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new Response(400, "Invalid input data", false);
            }

            var response = await _mediator.Send(request);

            _logger.LogInformation($"Student with ID {request.StudentId} removed");

            return response;
        }


        [HttpDelete("deleteCourse")]
        public async Task<ActionResult<Response>> DeleteCourse([FromBody] DeleteCourseCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid input data" });
            }

            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, new { Message = response.Message });
            }

            _logger.LogInformation($"Course with ID {command.CourseId} removed");

            return Ok(response);
        }

    }
}
