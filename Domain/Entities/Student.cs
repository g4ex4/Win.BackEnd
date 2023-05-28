using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Student : Person
    {
        public List<StudentCourse> StudentCourse { get; set; }
        public List<StudentSubscription> StudentSubscription { get; set; }

    }
}
