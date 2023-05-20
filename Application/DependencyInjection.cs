using Application.Courses.Commands.CreateCommands;
using Application.Courses.Commands.DeleteCommands;
using Application.Courses.Commands.UpdateCommands;
using Application.Courses.Queries.GetCourseDetails;
using Application.Courses.Queries.GetCourseList;
using Application.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication
            (this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
