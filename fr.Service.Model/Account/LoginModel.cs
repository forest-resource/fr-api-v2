using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Account
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[#?!@$%^&*-])[A-Za-z0-9#?!@$%^&*-]{8,}$")]
        public string Password { get; set; }
    }
}
