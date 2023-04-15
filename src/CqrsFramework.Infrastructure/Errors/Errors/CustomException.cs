using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure.Errors.Errors
{
    public class CustomException : Exception
    {
        public string Message { get; }

        public bool Success { get; }

        public CustomException(string message, bool success)
            : base(message)
        {
            Message = message;
            Success = success;
        }
    }
}
