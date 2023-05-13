using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Courses.Queries.GetCourseDetails
{
    public class CourseDetailsVm : IMapWith<Course>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseDetailsVm>();
        }
    }
}
