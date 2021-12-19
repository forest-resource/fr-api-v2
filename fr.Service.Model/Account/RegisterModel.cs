using System.ComponentModel.DataAnnotations;

namespace fr.Service.Model.Account
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string StudentCode { get; set; }
        
        [Required]
        public DateTime DayOfBird { get; set; }
    }
}
