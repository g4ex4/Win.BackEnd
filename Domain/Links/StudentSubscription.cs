using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Links
{
    public class StudentSubscription
    {
        public int StudenId { get; set; }
        public Student Student { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription {get; set;}
    }
}
