using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
