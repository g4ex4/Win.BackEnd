using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries.GetCourseList
{
    public class StudentCourseListVm
    {
        public int StudentId { get; set; }
        public IList<CourseLookupDto> Courses { get; set; }
    }
}
