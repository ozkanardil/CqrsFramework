using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsFramework.Infrastructure.Results
{
    public interface IRequestResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
