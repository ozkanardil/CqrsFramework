using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.Infrastructure.Results
{
    public interface IRequestDataResult<out T> : IRequestResult
    {
        T Data { get; }
    }
}
