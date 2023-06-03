using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class SubscriptionResponse : EntityResponse<int>
    {
        public SubscriptionResponse(Subscription subscription, int statusCode, string message, bool isSuccess)
            : base(subscription, statusCode, message, isSuccess)
        {
            if (subscription != null)
            {
                Id = subscription.Id;
                StudentId = subscription.StudentId;
                CourseId = subscription.CourseId;
            }
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
