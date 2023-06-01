using Application.Courses.Queries.GetCourseList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Queries
{
    public class MentorListVm
    {
        public IList<MentorLookupDto> Employees { get; set; }
    }
}
