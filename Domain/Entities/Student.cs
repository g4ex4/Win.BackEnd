using Domain.Common;
using Domain.Links;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Domain.Entities
{
    public class Student : Person
    {
        public List<StudentCourse> StudentCourse { get; set; }
        public List<StudentSubscription> StudentSubscription { get; set; }

    }
}
