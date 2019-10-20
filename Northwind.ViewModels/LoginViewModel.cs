using System.ComponentModel.DataAnnotations;


namespace Northwind.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "帳號必填")]
        [MinLength(4, ErrorMessage = "帳號最少四碼")]
        public string Username { get; set; }

        [Required(ErrorMessage = "密碼必填")]
        [MinLength(8, ErrorMessage = "密碼最少八碼")]
        public string Password { get; set; }
    }
}
