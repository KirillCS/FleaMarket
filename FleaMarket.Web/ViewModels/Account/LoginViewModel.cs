using System.ComponentModel.DataAnnotations;

namespace FleaMarket.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [MaxLength(256, ErrorMessage = "MaximumLengthError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredFieldError")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool IsPersistent { get; set; }

        public string ReturnUrl { get; set; }

        public LoginViewModel() { }

        public LoginViewModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}
