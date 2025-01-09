using System.ComponentModel.DataAnnotations;

namespace IT_Chronicles.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string  UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Please Enter Registered Password.")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
