using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServerDbContextConnection");
            var dbProvider = configuration.GetSection("DbProvider").Value;

            switch (dbProvider)
            {
                case "SqlServer":
                    services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));
                    services.AddScoped<BaseDbContext, SqlServerContext>();
                    services.AddScoped<ICourseDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IEmployeeDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IStudentDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<ICategoryDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IStudentSubscriptionDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IStudentCourseDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<ISubDbContext>(opt
                       => opt.GetService<BaseDbContext>());
                    services.AddScoped<ICoursesSubscriptionsDbContext>(opt
                       => opt.GetService<BaseDbContext>());
                    services.AddScoped<IVideoDbContext>(opt
                       => opt.GetService<BaseDbContext>());

                    using(var db = new SqlServerContext())
                    {
                        var (admin,isCreate) = db.SeedUsers();
                        if (isCreate == true)
                        {
                            db.Employees.Add(admin);
                            db.SaveChanges();
                        }
                    }

                    break;
                case "PostgreSQL":
                    services.AddDbContext<PgContext>(options => options.UseNpgsql(connectionString));
                    services.AddScoped<BaseDbContext, PgContext>();
                    services.AddScoped<ICourseDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IEmployeeDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IStudentDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<ICategoryDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IStudentSubscriptionDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<IStudentCourseDbContext>(opt
                        => opt.GetService<BaseDbContext>());
                    services.AddScoped<ISubDbContext>(opt
                       => opt.GetService<BaseDbContext>());
                    services.AddScoped<ICoursesSubscriptionsDbContext>(opt
                       => opt.GetService<BaseDbContext>());
                    services.AddScoped<IVideoDbContext>(opt
                       => opt.GetService<BaseDbContext>());

                    using (var db = new PgContext())
                    {
                        var (admin, isCreate) = db.SeedUsers();
                        if (isCreate == true)
                        {
                            db.Employees.Add(admin);
                            db.SaveChanges();
                        }
                    }
                    break;
                default:
                    throw new InvalidOperationException("Invalid database provider specified.");
            }
            


            return services;
        }
        
    }
}
