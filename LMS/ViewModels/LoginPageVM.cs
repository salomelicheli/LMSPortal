using System.ComponentModel.DataAnnotations;

namespace LMS.ViewModels
{
    public class LoginPageVM
    {
        [Required(ErrorMessage = "User ID is required.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Please, enter a valid personal number.")]
        public string userID { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
