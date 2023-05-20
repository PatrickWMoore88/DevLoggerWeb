using System.ComponentModel.DataAnnotations;

namespace DevLogger.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Password must be at least 10 characters")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
