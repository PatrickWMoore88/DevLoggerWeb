using System.ComponentModel.DataAnnotations;

namespace DevLogger.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(10, ErrorMessage = "Password must be at least 10 characters")]
        public string Password { get; set; }
    }
}
