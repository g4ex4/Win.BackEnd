using Domain.Common;
using Domain.Links;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subscription : BaseEntity<int>
    {
        public ILazyLoader Loader { get; set; }
        public int Id { get; set; }
        public DateTime DateSubscribed { get; set; }

        public List<CourseSubscription> CourseSubscription
        {
            get => Loader.Load(this, ref _courseSubscription);
            set => _courseSubscription = value;
        }
        private List<CourseSubscription> _courseSubscription;
        public List<StudentSubscription> StudentSubscription
        {
            get => Loader.Load(this, ref _studentSubscription);
            set => _studentSubscription = value;
        }
        private List<StudentSubscription> _studentSubscription;
    }
}
