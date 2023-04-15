using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.Infrastructure.Results
{
    public class SuccessRequestDataResult<T>:RequestDataResult<T>
    {
        public SuccessRequestDataResult(T data, string message) : base(data, true, message)
        {
        }

        public SuccessRequestDataResult(T data) : base(data, true)
        {
        }

        public SuccessRequestDataResult(string message) : base(default, true, message)
        {

        }

        public SuccessRequestDataResult() : base(default, true)
        {

        }
    }
}
