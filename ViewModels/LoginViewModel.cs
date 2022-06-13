using System.ComponentModel.DataAnnotations;

namespace Capstone.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }
}
