using System.ComponentModel.DataAnnotations;

namespace UserManagement.Client.Models
{
    public class UserForDisplay
    {
        public int Id { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "ФИО")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        [Display(Name = "Дата регистрации")]
        public DateOnly RegistrationDate { get; set; }
        [Display(Name = "Дата авторизации")]
        public DateOnly AuthorizationDate { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
    }
}
