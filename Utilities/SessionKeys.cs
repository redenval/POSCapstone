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

        public const string UserAccessStatus = "UserAccessStatus";
        public const string UserAccessStatusLoggedIn = "LoggedIn";
        public const string UserAccessStatusLoggedOut = "LoggedOut";

        public const string UserAccessRole = "UserAccessRole";
        public const string UserAccessRoleAdmin = "Admin";
        public const string UserAccessRoleDefault = "Default";

        //Data
        public const string User = "User";
    }
}
