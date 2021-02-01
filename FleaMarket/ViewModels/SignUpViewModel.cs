using System.ComponentModel.DataAnnotations;

namespace FleaMarket.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Name is the required field")]
        [MaxLength(256, ErrorMessage = "The maximum name lenght is 256 characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is the required field")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You must confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
