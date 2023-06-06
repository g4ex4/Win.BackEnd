using Application.Common.Mappings;
using Application.Courses.Queries.GetCourseList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.Queries
{
    public class CategoryLookupDto : IMapWith<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Category, CategoryLookupDto>();
        }
    }
}
