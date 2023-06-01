using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using Domain.Links;
using Microsoft.AspNetCore.Identity;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.Emit;

namespace Persistance
{
    public class BaseDbContext : DbContext, ICategoryDbContext, ICourseDbContext,
        IEmployeeDbContext, IStudentDbContext, ISubDbContext, IVideoDbContext, 
        IStudentSubscriptionDbContext, IStudentCourseDbContext, ICoursesSubscriptionsDbContext
    {
        public BaseDbContext()
        {
            Database.EnsureCreated();
        }
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Subscription> Subs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CategoryCourse> CategoryCourses { get; set; }
        public DbSet<CourseSubscription> CoursesSubscriptions { get; set;}
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentSubscription> StudentSubscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CategoryCourse>()
                .HasKey(cc => new { cc.CategoryId, cc.CourseId });

            builder.Entity<CourseSubscription>()
                .HasKey(cs => new { cs.CourseId, cs.SubscriptionId });

            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<StudentSubscription>()
                .HasKey(ss => new { ss.StudentId, ss.SubscriptionId });

            builder.Entity<Course>()
                .HasOne(c => c.Mentor)
                .WithMany(m => m.Courses)
                .HasForeignKey(c => c.MentorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Video>()
                .HasOne(v => v.Course)
                .WithMany(c => c.Videous)
                .HasForeignKey(v => v.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUser(builder);

        }
        private void SeedUser (ModelBuilder builder)
        {
            Employee admin = new Employee()
            {
                Id = 100,
                UserName = "Admin",
                Email = "1goldyshsergei1@gmail.com",
                EmailConfirmed = true,
                JobTitle = "Controle everyone and everything",
                Experience = "",
                Education = "",
                RoleId= 1,
                IsConfirmed= true,
            };
            PasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "SuperAdmin1!");
        }
        
    }
}
