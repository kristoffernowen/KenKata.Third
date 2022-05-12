using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KenKata.Shared.Models
{
    public class RegisterUserModel
    {
        [StringLength(25, ErrorMessage = "Username must be 5 to 25 characters long.", MinimumLength = 5)]
        [DisplayName("Username")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "You must submit an email.")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must submit a password.")]
        [DataType(DataType.Password), StringLength(50, ErrorMessage = "Must be at least 8 characters and less than 50.", MinimumLength = 8)]
        [RegularExpression(@"^((?=.*\d)(?=.*[A-Z]).{8,50})",
            ErrorMessage =
                "Password must be at least 8 characters, 1 uppercase letter, 1 special character, alphanumeric character")]

        public string Password { get; set; } = string.Empty;
    }
}
