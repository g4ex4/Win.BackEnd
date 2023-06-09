using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Subscription : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<CourseSubscription> CourseSubscriptions { get; set; }
        public DateTime DateSubscribed { get; set; }
    }
}
