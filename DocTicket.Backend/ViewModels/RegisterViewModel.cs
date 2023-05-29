using AutoMapper;
using DocTicket.Backend.Common.Mappings;
using DocTicket.Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace DocTicket.Backend.ViewModels
{
    public class RegisterViewModel : IMapWith
    {
        [Required(ErrorMessage = "Имя обязательное поле!")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Фамилия обязательное поле!")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Отчество обязательное поле!")]
        public string Patronymic { get; set; } = null!;

        [Required(ErrorMessage = "Email обязательное поле!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязательное поле!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтверждение пароля обязательное поле!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(AppUser), GetType());
        }
    }
}
