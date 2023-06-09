using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.PostgreSQL;

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

                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.MSSqlServer
                        (connectionString, sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Logs",
                            AutoCreateSqlTable = true
                        }
                        ).CreateLogger();
                            services.AddLogging(loggingBuilder => {
                                loggingBuilder.ClearProviders();
                                loggingBuilder.AddSerilog(Log.Logger);
                            });

                    using (var db = new SqlServerContext())
                    {
                        var (сategory, isCreateСategory) = db.SeedCategory();
                        if (isCreateСategory == true)
                        {
                            db.Categories.AddRange(сategory);
                            db.SaveChanges();
                        }

                        var (role, isCreateRoles) = db.SeedRole();
                        if (isCreateRoles == true)
                        {
                            db.Roles.AddRange(role);
                            db.SaveChanges();
                        }

                        var (admin, isCreate) = db.SeedUsers();
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
                        var (сategory, isCreateСategory) = db.SeedCategory();
                        if (isCreateСategory == true)
                        {
                            db.Categories.AddRange(сategory);
                            db.SaveChanges();
                        }

                        var (role, isCreateRoles) = db.SeedRole();
                        if (isCreateRoles == true)
                        {
                            db.Roles.AddRange(role);
                            db.SaveChanges();
                        }

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
