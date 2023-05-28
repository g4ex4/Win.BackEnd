using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries.GetCourseList
{
    public class GetStudentCoursesQuery : IRequest<StudentCourseListVm>
    {
        public int StudentId { get; set; }
    }
}
