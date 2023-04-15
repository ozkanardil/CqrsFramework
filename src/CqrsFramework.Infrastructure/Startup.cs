using CqrsFramework.Infrastructure.Errors.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure
{
    public static class Startup
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionMiddleware>();
    }
}
