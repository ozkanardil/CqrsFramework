using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.Infrastructure.Results
{
    public class SuccessRequestResult : RequestResult
    {
        public SuccessRequestResult(string message) : base(true, message)
        {
        }

        public SuccessRequestResult() : base(true)
        {
        }
    }
}
