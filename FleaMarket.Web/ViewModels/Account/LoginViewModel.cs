using System.ComponentModel.DataAnnotations;

namespace FleaMarket.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Name is the required field")]
        [MaxLength(256, ErrorMessage = "The maximum name lenght is 256 characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is the required field")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool IsPersistent { get; set; }

        public string ReturnUrl { get; set; }

        public LoginViewModel() { }

        public LoginViewModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}
