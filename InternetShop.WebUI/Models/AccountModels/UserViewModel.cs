using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models.AccountModels
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Emai")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Не верный формат")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия пользователя")]
        public string Surname { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PasswordHash { get; set; }

        [Display(Name = "Адрес пользователя")]
        public string Address { get; set; }

        public static UserViewModel Create(User user)
        {
            return new UserViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                PasswordHash = user.PasswordHash,
                Address = user.Address
            };
        }

        public User ToUser()
        {
            return new User
            {
                UserId = UserId ?? Guid.NewGuid().ToString(),
                Email = Email,
                Address = Address,
                Name = Name,
                Surname = Surname,
                PasswordHash = PasswordHash
            };
        }

        public IdentityUser ToIdentityUser()
        {
            return new IdentityUser(ToUser())
            {
                Password = Password
            };
        }
    }
}