using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DocTicket.Backend.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email обязателен.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
