using System.ComponentModel.DataAnnotations;

namespace LogApi.ViewModels
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}