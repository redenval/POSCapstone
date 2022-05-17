using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? OrderCreated { get; set; }
    }
}
