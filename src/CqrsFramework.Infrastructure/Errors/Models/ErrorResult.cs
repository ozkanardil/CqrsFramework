using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Infrastructure.Errors.Models
{
    public class ErrorResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
