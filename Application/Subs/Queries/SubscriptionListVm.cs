using Application.Courses.Queries.GetCourseList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Subs.Queries
{
    public class SubscriptionListVm
    {
        public IList<SubscriptionLookupDto> Subs { get; set; }
    }
}
