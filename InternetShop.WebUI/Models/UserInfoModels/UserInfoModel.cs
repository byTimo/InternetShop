using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models.UserInfoModels
{
    public class UserInfoModel
    {
        public string UserId { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public static UserInfoModel Create(User user)
        {
            return new UserInfoModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email =  user.Email,
                Address = user.Address,
                Orders = user.Orders
            };
        }
    }
}