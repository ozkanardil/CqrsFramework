using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Auth.Constants
{
    public static class Messages
    {
        public static string UserNotFound = "User not found.";
        public static string UserLoginOk = "Login succesfull.";
        public static string UserLoginErr = "Login error.";
    }
}
