using Application.Courses.Queries.GetCourseList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Subs.Queries
{
    public class GetSubscriptionListQuery : IRequest<SubscriptionListVm>
    {
        public int StudentId { get; set; }
    }
}
