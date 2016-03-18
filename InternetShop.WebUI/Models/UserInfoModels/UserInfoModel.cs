using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models.UserInfoModels
{
    public class UserInfoModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public IEnumerable<Order> Orders { get; set; }


        public static UserInfoModel Create(User user)
        {
            return new UserInfoModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Address = user.Address,
                Orders = user.Orders
            };
        }
    }
}