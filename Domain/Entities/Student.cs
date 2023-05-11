using Domain.Common;
using Domain.Links;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Domain.Entities
{
    public class Student : Person
    {
        private ILazyLoader Loader { get; set; }
        public List<StudentCourse> StudentCourse
        {
            get => Loader.Load(this, ref _studentCourse);
            set => _studentCourse = value;
        }
        private List<StudentCourse> _studentCourse;
        public List<StudentSubscription> StudentSubscription
        {
            get => Loader.Load(this, ref _studentSubscription);
            set => _studentSubscription = value;
        }
        private List<StudentSubscription> _studentSubscription;

    }
}
