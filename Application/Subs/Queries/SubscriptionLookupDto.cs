using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Subs.Queries
{
    public class SubscriptionLookupDto : IMapWith<Subscription>
    {
        public DateTime DateSubscribed { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubscriptionLookupDto>();
        }
    }
}
