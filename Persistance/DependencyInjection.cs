using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    break;
                case "PostgreSQL":
                    services.AddDbContext<PgContext>(options => options.UseNpgsql(connectionString));
                    services.AddScoped<BaseDbContext, PgContext>();
                    break;
                default:
                    throw new InvalidOperationException("Invalid database provider specified.");
            }
            


            return services;
        }
        //public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var connectionString = configuration["SqlServerDbContextConnection"];
        //    services.AddDbContext<BaseDbContext>(options =>
        //    {
        //        options.UseNpgsql(connectionString);
        //    });
        //    services.AddScoped<BaseDbContext>();
        //    return services;
        //}
        //public static IServiceCollection AddPersistance(this IServiceCollection
        //    services, IConfiguration configuration)
        //{
        //    var connectionString = configuration.GetConnectionString("SqlServerDbContextConnection");
        //    var dbProvider = configuration.GetSection("DbProvider").Value;

        //    //var connectionString = configuration["ConnectionString"];
        //    services.AddDbContext<BaseDbContext>(options =>
        //    {
        //        options.UseSqlServer(connectionString);
        //    });
        //    services.AddScoped<ICourseDbContext>(opt
        //        => opt.GetService<BaseDbContext>());
        //    return services;
        //}
    }
}
