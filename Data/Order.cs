using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
    }
}
