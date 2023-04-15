using CqrsFramework.Infrastructure.Errors.Errors;
using CqrsFramework.Infrastructure.Errors.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CqrsFramework.Infrastructure.Errors.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                //string errorId = Guid.NewGuid().ToString();
                //LogContext.PushProperty("ErrorId", errorId);
                //LogContext.PushProperty("StackTrace", exception.StackTrace);
                var errorResult = new ErrorResult
                {
                    Success = false,
                    Message = exception.Message,
                };
                //errorResult.Messages.Add(exception.Message);

                //if (exception is not CustomException && exception.InnerException != null)
                //{
                //    while (exception.InnerException != null)
                //    {
                //        exception = exception.InnerException;
                //    }
                //}

                //switch (exception)
                //{
                //    case CustomException e:
                //        errorResult.StatusCode = (int)e.StatusCode;
                //        if (e.ErrorMessages is not null)
                //        {
                //            errorResult.Messages = e.ErrorMessages;
                //        }

                //        break;

                //    case KeyNotFoundException:
                //        errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                //        break;

                //    default:
                //        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                //        break;
                //}

                //Log.Error($"{errorResult.Exception} Request failed with Status Code {context.Response.StatusCode} and Error Id {errorId}.");
                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
                }
                //else
                //{
                //    Log.Warning("Can't write error response. Response has already started.");
                //}
            }
        }
    }
}
