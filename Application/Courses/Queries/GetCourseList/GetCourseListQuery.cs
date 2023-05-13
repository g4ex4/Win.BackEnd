using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries.GetCourseList
{
    public class GetCourseListQuery : IRequest<CourseListVm>
    {
        public int MentorId { get; set; }

    }
}
