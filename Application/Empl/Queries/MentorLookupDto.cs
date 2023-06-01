using Application.Common.Mappings;
using Application.Courses.Queries.GetCourseList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Queries
{
    public class MentorLookupDto : IMapWith<Employee>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeUpdated { get; set; }
        public string JobTitle { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public bool IsConfirmed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Employee, MentorLookupDto>();
        }
    }
}
