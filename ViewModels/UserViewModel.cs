using System.ComponentModel.DataAnnotations;

namespace Capstone.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Profile { get; set; }
        [Display(Name = "Email Address")]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Minimum eight characters, at least one letter and one number")]
        [Required]
        public string Password { get; set; }
        [RegularExpression(@"^(09|\+639|639)\d{9}$", ErrorMessage = "Please provide a valid phone number")]
        [Display(Name = "Phone Number")]
        [Required]
        public string Phone { get; set; }
        [Display(Name = "Street Address")]
        [Required]
        public string StreetAddress { get; set; }
        [Display(Name = "Barangay")]
        [Required]
        public string Barangay { get; set; }
    }
}
