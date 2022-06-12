using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        public Product Product { get; set; }
    }
}
