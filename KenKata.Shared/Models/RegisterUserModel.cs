using System.ComponentModel.DataAnnotations;

namespace KenKata.Shared.Models
{
    public class RegisterUserModel
    {
        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password), StringLength(32, ErrorMessage = "Must be at least 8 characters.")]
        [RegularExpression(@"^((?=.*\d)(?=.*[A-Z]).{8,50})",
            ErrorMessage =
                "Must be at least 8 characters, 1 uppercase letter, 1 special character, alphanumeric character")]

        public string Password { get; set; } = string.Empty;
    }
}
