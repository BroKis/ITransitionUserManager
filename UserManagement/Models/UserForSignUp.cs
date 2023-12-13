using System.ComponentModel.DataAnnotations;
using UserManagement.IdentityDAL.Auxillary;

namespace UserManagement.Client.Models
{
    public class UserForSignUp
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "ФИО")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Номер телефона")]
        [StringLength(16, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов", MinimumLength = 10)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов", MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароль не одинаковы")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        public DateOnly RegistrationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly AuthorizationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public string Status { get; set; } = IdentityDAL.Auxillary.Status.Active.ToString();

    }
}
