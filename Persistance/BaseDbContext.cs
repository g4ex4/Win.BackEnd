using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Domain.Entities;
using Domain.Links;

namespace Persistance
{
    public class BaseDbContext : DbContext, ICategoryDbContext, ICourseDbContext,
        IEmployeeDbContext, IStudentDbContext, ISubDbContext, IVideoDbContext
    {
        public BaseDbContext()
        {
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
                .HasKey(ss => new { ss.StudenId, ss.SubscriptionId });

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

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    IConfiguration config = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var connectionString = config.GetConnectionString("ConnectionString")
        //        ?? throw new InvalidOperationException(
        //            "Connection string 'PgDbContextConnection' not found.");

        //    optionsBuilder.UseNpgsql(connectionString, builder =>
        //    {
        //        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //    });
        //}
    }
}
