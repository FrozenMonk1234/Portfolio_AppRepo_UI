using System.ComponentModel.DataAnnotations;

namespace Portfolio_AppRepo_UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
