using Application.Empl.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Queries
{
    public class StudentListVm
    {
        public IList<StudentLookupDto> Students { get; set; }
    }
}
