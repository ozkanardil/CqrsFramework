using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.Infrastructure.Results
{
    public class RequestDataResult<T> : RequestResult, IRequestDataResult<T>
    {
        public RequestDataResult(T data,bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public RequestDataResult(T data, bool success):base(success)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
