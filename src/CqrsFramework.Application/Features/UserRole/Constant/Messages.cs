using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.UserRole.Constant
{
    public static class Messages
    {
        public static string UserRoleAddError = "User role could not be created.";
        public static string UserRoleAddSuccess = "User role has been created.";
        public static string UserRoleNoRecord = "No User Role record found.";
        public static string UserRolesListed = "User Roles have been listed.";
        public static string UserRoleDeleteError = "User Role could not be deleted.";
        public static string UserRoleDeleted = "User Role has been deleted.";
    }
}
