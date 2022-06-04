using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Role { get; set; }
        public string Profile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string Barangay { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
