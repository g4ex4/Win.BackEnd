using Domain.Entities;

namespace Domain.Links
{
    public class StudentSubscription
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription {get; set;}
    }
}
