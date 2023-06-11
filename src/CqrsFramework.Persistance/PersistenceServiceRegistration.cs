using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;

namespace CqrsFramework.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            string myConnStr = ApplicationSettings.DbConnString;
            services.AddDbContext<DatabaseContext>(options => options.UseMySql(myConnStr,
                                                                            ServerVersion.AutoDetect(myConnStr),
                                                                            b => b.MigrationsAssembly("CqrsFramework.Api")));
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
