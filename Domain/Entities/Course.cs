using Domain.Common;
using Domain.Links;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Entities
{
    public class Course : BaseEntity<int>
    {
        private ILazyLoader Loader { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
        public List<StudentCourse> StudentCourse
        {
            get => Loader.Load(this, ref _studentCourse);
            set => _studentCourse = value;
        }
        private List<StudentCourse> _studentCourse;
        public List<CategoryCourse> CategoryCourse
        {
            get => Loader.Load(this, ref _categoryCourse);
            set => _categoryCourse = value;
        }
        private List<CategoryCourse> _categoryCourse;
    }
}
