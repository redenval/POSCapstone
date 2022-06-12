using System.ComponentModel.DataAnnotations;

namespace Capstone.Data
{
    public class ProductOrder
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public Order Order { get; set; }
    }
}
