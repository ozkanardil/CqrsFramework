using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.User.Constants
{
    public static class Messages
    {
        public static string UserAddError = "User could not be created.";
        public static string UserAddSuccess = "User has been created.";
        public static string UserNoRecord = "No user record found.";
        public static string UserListed = "Users have been listed.";
        public static string UserDeleteError = "Users could not be deleted.";
        public static string UserDeleted = "Users has been deleted.";
        public static string UserNotFound = "User not found.";
    }
}
