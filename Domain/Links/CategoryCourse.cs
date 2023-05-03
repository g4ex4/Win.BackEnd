using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Links
{
    public class CategoryCourse
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
