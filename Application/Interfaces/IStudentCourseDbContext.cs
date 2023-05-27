using Domain.Links;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentCourseDbContext
    {
        DbSet<StudentCourse> StudentCourses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
