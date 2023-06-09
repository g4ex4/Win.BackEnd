using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using Domain.Links;
using Microsoft.AspNetCore.Identity;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistance
{
    public class BaseDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>,
         ICategoryDbContext, ICourseDbContext,
         ISubDbContext, IVideoDbContext, 
         IUserCourseDbContext, ICoursesSubscriptionsDbContext
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<CategoryCourse> CategoryCourses { get; set; }
        public DbSet<CourseSubscription> CoursesSubscriptions { get; set;}
        public DbSet<UserCourse> UserCourses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CategoryCourse>()
                .HasKey(cc => new { cc.CategoryId, cc.CourseId });
            builder.Entity<CategoryCourse>()
                .HasOne(a => a.Course)
                .WithMany(ba => ba.CategoryCourses)
                .HasForeignKey(a => a.CourseId);
            builder.Entity<CategoryCourse>()
                            .HasOne(a => a.Category)
                            .WithMany(ba => ba.CategoryCourses)
                            .HasForeignKey(a => a.CategoryId)
                            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<CourseSubscription>()
                .HasKey(cs => new { cs.CourseId, cs.SubscriptionId });
            builder.Entity<CourseSubscription>()
                .HasOne(a => a.Course)
                .WithMany(ba => ba.CourseSubscriptions)
                .HasForeignKey(a => a.CourseId);
            builder.Entity<CourseSubscription>()
                            .HasOne(a => a.Subscription)
                            .WithMany(ba => ba.CourseSubscriptions)
                            .HasForeignKey(a => a.SubscriptionId)
                            .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<UserCourse>()
                .HasKey(sc => new { sc.UserId, sc.CourseId });


            builder.Entity<UserCourse>()
                .HasOne(u => u.User)
                .WithMany(c => c.UserCourse)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserCourse>()
                .HasOne(u => u.Course)
                .WithMany(c => c.UserCourses)
                .HasForeignKey(u => u.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Video>()
                .HasOne(v => v.Course)
                .WithMany(c => c.Videous)
                .HasForeignKey(v => v.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }

        
        public (List<Role>?, bool) SeedRole()
        {
            if (Roles.Any())
                return (null, false);
            Role role = new Role()
            {
                
                RoleName = "Admin"
            };
            Role role1 = new Role()
            {
                
                RoleName = "Employee"
            };
            List<Role> roles = new List<Role>();
            roles.Add(role);
            roles.Add(role1);

            return (roles, true);

        }

        public (List<Category>?, bool) SeedCategory()
        {
            if (Categories.Any())
                return (null, false);
            Category сategory = new Category()
            {
                Name = "Frontend development"
            };
            Category сategory1 = new Category()
            {
                Name = "Backend development"
            };
            List<Category> сategories = new List<Category>();
            сategories.Add(сategory);
            сategories.Add(сategory1);

            return (сategories, true);

        }


    }
}
