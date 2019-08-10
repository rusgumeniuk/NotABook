using System.ComponentModel.DataAnnotations;

namespace NotABook.WebAppCore.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(55, ErrorMessage = "Hmmm, we can't store your password because we even couldn't image that it can has this length. Try other length please.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "No way... Your passwords have differences. \r\n But they should be same, so please try again")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
