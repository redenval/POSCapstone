using Capstone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItemViewModel> Cart { get; set; }
        public UserViewModel user;
    }

    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
