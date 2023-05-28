using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Subscription : BaseEntity<int>
    {
        public DateTime DateSubscribed { get; set; }

        public List<CourseSubscription> CourseSubscription { get; set; }
        private List<CourseSubscription> _courseSubscription;
        public List<StudentSubscription> StudentSubscription { get; set; }
        private List<StudentSubscription> _studentSubscription;
    }
}
