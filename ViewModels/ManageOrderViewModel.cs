using System.Collections.Generic;

namespace Capstone.ViewModels
{
    public class ManageOrderViewModel
    {
        public IEnumerable<OrderViewModel> PendingOrders { get; set; }
        public IEnumerable<OrderViewModel> ShippedOrders { get; set; }
        public IEnumerable<OrderViewModel> DeliveredOrders { get; set; }
    }
}
