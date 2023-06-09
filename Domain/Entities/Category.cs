using Domain.Common;
using Domain.Links;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category: BaseEntity<int>
    {
        public string Name { get; set; }
        public List<CategoryCourse> CategoryCourses { get; set; }

    }
}
