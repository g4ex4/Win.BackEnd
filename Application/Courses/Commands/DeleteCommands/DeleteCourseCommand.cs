using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Commands.DeleteCommands
{
    public class DeleteCourseCommand : IRequest<Response>
    {
        [Required(ErrorMessage = "CourseId is required.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "MentorId is required.")]
        public int MentorId { get; set; }
    }
}
