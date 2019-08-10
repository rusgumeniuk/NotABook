using System.ComponentModel.DataAnnotations;

namespace NotABook.WebAppCore.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Oh, u missed your username :c \r\n Input it please")]
        public string Username { get; set; }

        [Required(ErrorMessage = "We know that u have password, so u need to input it")]        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
