using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KenKata.Shared.Models
{
    public class SignInUserModel
    {
        public string UserNameOrEmail { get; set; } = string.Empty;

       

        public string Password { get; set; } = string.Empty;
    }
}