using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Subscription : BaseEntity<int>
    {
        public DateTime DateSubscribed { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
