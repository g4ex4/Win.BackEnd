using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<CategoryCourse> CategoryCourse { get; set; }
    }
}
