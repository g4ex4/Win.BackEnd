using Domain.Common;

namespace Domain.Entities
{
    public class Employee : Person
    {
        public string JobTitle { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public bool IsConfirmed { get; set; }
        public List<Course> Courses { get; set; } 
    }
}
