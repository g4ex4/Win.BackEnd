using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries.GetCourseList
{
    public class CourseListVm
    {
        public IList<CourseLookupDto> Courses { get; set; }
    }
}
