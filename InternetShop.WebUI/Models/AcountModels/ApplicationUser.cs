using System;
using InternetShop.DataLayer.Entities;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Models.AcountModels
{
    public class ApplicationUser : IUser
    {
        public string Id { get; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public ApplicationUser(string name)
        {
            Id = Guid.NewGuid().ToString();
            UserName = name;
        }

        public ApplicationUser(User user)
        {
            Id = user.UserId;
            UserName = user.Name;
            Surname = user.Surname;
            PasswordHash = user.PasswordHash;
            Address = user.Address;
            Email = user.Email;
        }

        public User ToUserEntity()
        {
            return new User
            {
                UserId = Id,
                Name = UserName,
                Surname = Surname,
                PasswordHash = PasswordHash,
                Email = Email,
                Address = Address
            };
        }
    }
}