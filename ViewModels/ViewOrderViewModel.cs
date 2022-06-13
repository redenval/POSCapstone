using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class ViewOrderViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }


    public class OrderViewModel
    {
        public int Id { get; set; }
        public ProfileViewModel Profile { get; set; }
        public string Date { get; set; }
        public double Total { get; set; }
        public IEnumerable<ProductOrderViewModel> ProductOrders { get; set; }

    }

    public class ProductOrderViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
