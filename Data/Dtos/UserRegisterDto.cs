using System.ComponentModel.DataAnnotations;

namespace StockProject.Data.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "UserName missing")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password missing"), DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public UserRole Role { get; set; }

    }
}
