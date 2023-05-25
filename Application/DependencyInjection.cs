using Application.Courses.Commands.CreateCommands;
using Application.Courses.Commands.DeleteCommands;
using Application.Courses.Commands.UpdateCommands;
using Application.Courses.Queries.GetCourseDetails;
using Application.Courses.Queries.GetCourseList;
using Application.Interfaces;
using Application.JWT;
using Domain.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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
            //services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            //var SecretKey = Configuration.GetSection("JwtSettings:SecretKey").Value;
            //var Issuer = Configuration.GetSection("JwtSettings:Issuer").Value;
            //var Audience = Configuration.GetSection("JwtSettings:Audience").Value;
            return services;
        }
    }
}
