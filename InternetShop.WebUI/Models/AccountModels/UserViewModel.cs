using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InternetShop.WebUI.Models.AccountModels
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)] 
        public string UserId { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Emai")]
        [DataType(DataType.Password, ErrorMessage = "Не верный формат")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия пользователя")]
        public string Surname { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PasswordHash { get; set; }

        [Display(Name = "Адрес пользователя")]
        public string Address { get; set; }

        public static UserViewModel Create(ApplicationUser user)
        {
            return new UserViewModel
            {
                UserId = user.Id,
                Email = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                PasswordHash = user.PasswordHash,
                Address = user.Address
            };
        }

        public ApplicationUser ToApplicationUser()
        {
            return ToApplicationUser(new ApplicationUser(Email));
        }

        public ApplicationUser ToApplicationUser(ApplicationUser user)
        {
            user.Name = Name;
            user.Surname = Surname;
            user.Address = Address;
            user.PasswordHash = PasswordHash;
            return user;
        }
    }
}