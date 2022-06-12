using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.ViewModels
{
    public class StoreViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

    }
}
