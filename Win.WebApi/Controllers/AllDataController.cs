using Application.Courses.Queries.GetCourseList;
using Application.Empl.Queries;
using Application.Students.Queries;
using Application.Subs.Queries;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AllDataController(IMediator mediator)
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

        [HttpGet("getAllCourses")]
        public async Task<ActionResult<CourseListVm>> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var courseList = await _mediator.Send(query);

            var response = new Response(200, "Courses retrieved successfully", true);
            return Ok(new { Response = response, Courses = courseList });
        }

        [HttpGet("getAllStudents")]
        public async Task<ActionResult<List<StudentLookupDto>>> GetAllStudents()
        {
            var query = new GetAllStudentsQuery();
            var mentorsList = await _mediator.Send(query);

            var response = new Response(200, "Students retrieved successfully", true);
            return Ok(new { Response = response, Students = mentorsList });
        }

        [HttpGet("getAllSubscription")]
        public async Task<ActionResult<SubscriptionListVm>> GetAllSubscription()
        {
            var query = new GetAllSubscriptionQuery();
            var subsList = await _mediator.Send(query);

            var response = new Response(200, "Subscriptions retrieved successfully", true);
            return Ok(new { Response = response, Subs = subsList });
        }
    }
}
