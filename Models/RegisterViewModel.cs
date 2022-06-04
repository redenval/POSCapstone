using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class RegisterViewModel
    {
        [Email]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
    }
}
