using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            string myConnStr = ApplicationSettings.DbConnString;
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(myConnStr));
            //services.AddScoped<IBrandRepository, BrandRepository>();
            //services.AddScoped<IModelRepository, ModelRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            //services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            //services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

            return services;
        }
    }
}
