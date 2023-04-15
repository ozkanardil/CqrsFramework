using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.OrderItem.Constants
{
    public static class Messages
    {
        public static string OrderItemAddError = "Order Item could not be created.";
        public static string OrderItemAddSuccess = "Order Item has been created.";
        public static string OrderItemNoRecord = "No Order Item record found.";
        public static string OrderItemsListed = "Order Items have been listed.";
        public static string OrderItemDeleteError = "Order Item could not be deleted.";
        public static string OrderItemDeleted = "Order Item has been deleted.";
    }
}
