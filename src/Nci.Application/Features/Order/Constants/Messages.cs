using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.Order.Constants
{
    public static class Messages
    {
        public static string OrderAddError = "Order could not be created.";
        public static string OrderAddSuccess = "Order has been created.";
        public static string OrderNoRecord = "No Order record found.";
        public static string OrdersListed = "Orders have been listed.";
        public static string OrderDeleteError = "Order could not be deleted.";
        public static string OrderDeleted = "Order has been deleted.";
    }
}
