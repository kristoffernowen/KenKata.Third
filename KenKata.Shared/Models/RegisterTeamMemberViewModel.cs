using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenKata.Shared.Models
{
    public class RegisterTeamMemberViewModel
    {
        public RegisterUserModel RegisterUserModel { get; set; }

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
    }
}
