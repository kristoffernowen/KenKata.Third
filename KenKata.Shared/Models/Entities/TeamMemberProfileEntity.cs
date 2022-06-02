
using System.ComponentModel.DataAnnotations;


namespace KenKata.Shared.Models.Entities
{
    public class TeamMemberProfileEntity
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must submit a firstname")]
        [Display(Name = "First name")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{1,}$",
            ErrorMessage = "Must be a valid name")]

        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "You must submit a lastname")]
        [Display(Name = "Last name")]
        [RegularExpression(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$",
            ErrorMessage = "Must be a valid name")]

        public string LastName { get; set; } = null!;

        [Required] public string Title { get; set; } = null!;

        public string ProfilePhotoFileName { get; set; } = string.Empty;
        [Required] public string UserId { get; set; } = null!;

    }
}
