using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectricStore.Common
{
    public struct SessionData
    {
        public const string SESSION_USER_NAME = "SESSION_USER_NAME";
        public const string SESSION_USER_PASSWORD = "SESSION_USER_PASSWORD";
        public const string SESSION_CART = "SESSION_CART";
        public const string SESSION_CART_QUANTITY = "SESSION_CART_QUANTITY";
        public const string SESSION_FIRST_TIME = "SESSION_FIRST_TIME";
        public const string SESSION_LAST_TIME = "SESSION_LAST_TIME";
    }
}