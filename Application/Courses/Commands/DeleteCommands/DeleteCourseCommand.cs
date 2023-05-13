using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.DeleteCommands
{
    public class DeleteCourseCommand : IRequest<Response>
    {
        public int CourseId { get; set; }
        public int MentorId { get; set; }
    }
}
