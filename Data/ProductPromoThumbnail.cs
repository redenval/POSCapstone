using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data
{
    public class ProductPromoThumbnail
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public Product Product { get; set; }
    }
}
