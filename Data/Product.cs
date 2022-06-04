using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Category")]
        public Category BaseCategory { get; set; }
        [ForeignKey("ProductThumbnail")]
        public ProductThumbnail BaseImage { get; set; }
        public ICollection<ProductImage> Album { get; set; }
        public string BaseName { get; set; }
        public double BasePrice { get; set; }
        public bool PromoStatus { get; set; }
        public string PromoName { get; set; }
        public string PromoImage { get; set; }
        public double PromoPrice { get; set; }
        public int PromoQuantity { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } 
    }
}
