using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Courses.Queries.GetCourseList
{
    public class CourseLookupDto : IMapWith<Course>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MentorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseLookupDto>();
        }
    }
}
