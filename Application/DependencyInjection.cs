using Application.Managers;
using Application.Repositories;
using Application.Services;
using Application.Users;
using MediatR;
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
