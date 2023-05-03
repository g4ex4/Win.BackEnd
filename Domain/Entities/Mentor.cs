using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mentor : BaseEntity<int>
    {
        public string JobTitle { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public bool IsConfirmed { get; set; }
        public List<Course> Courses { get; set; } 
    }
}
