using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Infrastructure.Security.JwtToken;
using CqrsFramework.Infrastructure.Errors.Middleware;
using CqrsFramework.Infrastructure.CustomExceptionFilter;
using CqrsFramework.Infrastructure.LogEntries;

namespace CqrsFramework.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<ExceptionMiddleware>();

            services.AddTransient<LogFilter>();
            services.AddTransient<ExceptionFilter>();

            return services;

        }
    }
}
