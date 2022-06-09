using System.ComponentModel.DataAnnotations;

namespace Capstone.Data
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public User User { get; set; }
    }
}
