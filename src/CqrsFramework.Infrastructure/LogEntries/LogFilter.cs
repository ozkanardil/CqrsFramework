using CqrsFramework.Domain.Entities;
using CqrsFramework.Persistance.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure.LogEntries
{
    public class LogFilter : IActionFilter
    {
        private readonly DatabaseContext _context;

        public LogFilter(DatabaseContext context)
        {
            _context = context;
        }

        LogEntity logEntity = new LogEntity();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                logEntity.id = 0;
                logEntity.username = context.HttpContext.User.Identity.IsAuthenticated == false ? "Ziyaretçi" : context.HttpContext.User.Identity.Name.ToString();
                var userClaims = context.HttpContext.User.Claims.ToList();
                string userEmail = userClaims[1].Value;
                logEntity.email = context.HttpContext.User.Identity.IsAuthenticated == false ? "-" : userEmail;
                logEntity.ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
                logEntity.logdate = DateTime.Now;
                logEntity.controller = context.ActionDescriptor.DisplayName;
                var myParameters = context.ActionArguments;
                string myParamsForResult = "";
                logEntity.parameters = myParameters.Aggregate(myParamsForResult, (current, p) => current + (p.Key + ": " + p.Value + Environment.NewLine));
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string myStatusCode = context.HttpContext.Response.StatusCode.ToString();
                logEntity.status = myStatusCode;
                logEntity.description = "-";
                logEntity.type = myStatusCode == "200" ? EnumLogType.Log.ToString() : EnumLogType.Error.ToString();

                _context.Logs.Add(logEntity);
                _context.SaveChanges();
            }
        }
    }
}
