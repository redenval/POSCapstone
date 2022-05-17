using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Utilities
{
    public static class SessionKeys
    {
        public const string OrderStatus = "OrderStatus";
        public const string OrderStatusPending = "Pending";
        public const string OrderStatusApproved = "Approved";
        public const string OrderStatusDelivered = "Delivered";

        public const string UserAccess = "UserAccess";
        public const string UserAccessAdmin = "Admin";
        public const string UserAccessDefault = "Default";
    }
}
