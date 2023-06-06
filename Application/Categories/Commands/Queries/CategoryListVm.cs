using Application.Courses.Queries.GetCourseList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.Queries
{
    public class CategoryListVm
    {
        public IList<CategoryLookupDto> Categories { get; set; }
    }
}
