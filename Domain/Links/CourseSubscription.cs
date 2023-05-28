using Domain.Entities;

namespace Domain.Links
{
    public class CourseSubscription
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
