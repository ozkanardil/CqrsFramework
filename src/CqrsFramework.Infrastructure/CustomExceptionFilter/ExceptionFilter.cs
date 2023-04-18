using CqrsFramework.Domain.Entities;
using CqrsFramework.Infrastructure.LogEntries;
using CqrsFramework.Persistance.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure.CustomExceptionFilter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly DatabaseContext _context;

        public ExceptionFilter(DatabaseContext context)
        {
            _context = context;
        }

        public void OnException(ExceptionContext context)
        {
            LogEntity logEntity = new LogEntity();

            logEntity.id = 0;
            logEntity.username = context.HttpContext.User.Identity.IsAuthenticated == false ? "Ziyaretçi" : context.HttpContext.User.Identity.Name.ToString();
            logEntity.ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            logEntity.logdate = DateTime.Now;
            logEntity.controller = context.ActionDescriptor.DisplayName;
            logEntity.parameters = "-";
            string myStatusCode = context.HttpContext.Response.StatusCode.ToString();
            logEntity.status = myStatusCode;
            string errorMessage = context.Exception.Message;
            //string stackTrace = context.Exception.StackTrace;
            logEntity.description = string.Format("Message: {0}", errorMessage);
            logEntity.type = EnumLogType.Error.ToString();

            _context.Logs.Add(logEntity);
            _context.SaveChanges();
        }
    }
}
