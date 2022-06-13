using Capstone.Utilities;
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
        public ProductCategory Category { get; set; }
        [ForeignKey("ProductBaseImages")]
        public ProductBaseImage BaseImage { get; set; }
        public string BaseName { get; set; }
        public double BasePrice { get; set; }
        public bool PromoStatus { get; set; }
        public string PromoName { get; set; }
        public string Description { get; set; }
        [ForeignKey("ProductPromoImages")]
        public ProductPromoImage PromoImage { get; set; }
        public double PromoPrice { get; set; }
        public int PromoQuantity { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; } 
        public ICollection<CartProduct> CartProducts { get; set; } 
    }
}
