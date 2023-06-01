using Application.Empl.Commands.DeleteCommands;
using Application.Empl.Queries;
using Application.Students.Queries;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "1")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<ActionResult<MentorLookupDto>> GetMentorById(int id)
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
        public async Task<ActionResult<StudentLookupDto>> GetStudentById(int id)
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
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpDelete("Delete-Student")]
        public async Task<Response> DeleteStudent(DeleteStudentCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

    }
}
