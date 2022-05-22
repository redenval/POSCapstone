using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public List<ItemViewModel> Items { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
