using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.Queries
{
    public class StudentLookupDto : IMapWith<Student>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentLookupDto>();
        }
    }
}
