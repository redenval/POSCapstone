using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data.Entities
{
    public class CustomerOrderHistory
    {
        [Key]
        public int Id { get; set; }
        public Guid OrderId { get; set; }
    }
}
