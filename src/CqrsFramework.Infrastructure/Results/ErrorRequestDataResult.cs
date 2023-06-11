using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.Infrastructure.Results
{
    public class ErrorRequestDataResult<T> : RequestDataResult<T>
    {
        public ErrorRequestDataResult(T data, string message) : base(data, false, message)
        {
        }

        public ErrorRequestDataResult(T data) : base(data, false)
        {
        }

        public ErrorRequestDataResult(string message) : base(default, false, message)
        {

        }

        public ErrorRequestDataResult() : base(default, false)
        {

        }
    }
}
