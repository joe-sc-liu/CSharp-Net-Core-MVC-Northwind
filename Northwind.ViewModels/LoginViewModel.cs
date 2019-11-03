
using System.ComponentModel.DataAnnotations;

namespace Northwind.ViewModels
{
    public class LoginViewModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Account.Required")]
        [MinLength(4, ErrorMessage = "帳號最少四碼")]
        public string Username { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(ErrorMessage = "Pwd.Required")]
        [MinLength(8, ErrorMessage = "密碼最少八碼")]
        public string Password { get; set; }
    }

}
