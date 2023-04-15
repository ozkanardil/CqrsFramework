using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsFramework.Application.Features.ShoppingCart.Constants
{
    public static class Messages
    {
        public static string ShoppingCartAddError = "Shopping Cart could not be created.";
        public static string ShoppingCartAddSuccess = "Shopping Cart has been created.";
        public static string ShoppingCartNoRecord = "No Shopping Cart record found.";
        public static string ShoppingCartListed = "Shopping Carts have been listed.";
        public static string ShoppingCartDeleteError = "Shopping Carts could not be deleted.";
        public static string ShoppingCartDeleted = "Shopping Carts has been deleted.";
        public static string ShoppingCartNotFound = "Shopping Cart not found.";
    }
}
