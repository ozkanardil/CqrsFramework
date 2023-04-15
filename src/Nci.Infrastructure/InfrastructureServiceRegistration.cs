using Microsoft.Extensions.DependencyInjection;
using CqrsFramework.Infrastructure.Security.JwtToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure
{
     public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ITokenHelper, JwtHelper>();

            return services;

        }
    }
}
