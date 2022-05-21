using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data.Entities
{
    public class ProductItem
    {
        [Key]
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
