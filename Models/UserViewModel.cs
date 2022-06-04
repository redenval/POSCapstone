using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Profile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string Barangay { get; set; }
    }
}
