using System.ComponentModel.DataAnnotations;

namespace FleaMarket.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [MaxLength(256, ErrorMessage = "MaximumLengthError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "MustConfirm")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm")]
        [Compare("Password", ErrorMessage = "DontMatch")]
        public string ConfirmPassword { get; set; }
    }
}
