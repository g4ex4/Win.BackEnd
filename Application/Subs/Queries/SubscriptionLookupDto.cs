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
        public string Title { get; set; }
        public string Description { get; set; }
        public string MentorName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Subscription, SubscriptionLookupDto>()
                .ForMember(cfg => cfg.Title, opt => opt.MapFrom(n => n.Course.Title))
                .ForMember(cfg => cfg.Description, opt => opt.MapFrom(n => n.Course.Description))
                .ForMember(cfg => cfg.MentorName, opt => opt.MapFrom(n => n.Course.Mentor.UserName));

        }
    }
}
