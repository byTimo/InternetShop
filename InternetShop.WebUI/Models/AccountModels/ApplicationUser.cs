using System;
using InternetShop.DataLayer.Entities;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Models.AccountModels
{
    public class ApplicationUser : IUser
    {
        public string Id { get; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public ApplicationUser(string email)
        {
            Id = Guid.NewGuid().ToString();
            UserName = email;
        }

        public ApplicationUser(User user)
        {
            Id = user.UserId;
            UserName = user.Email;
            Surname = user.Surname;
            PasswordHash = user.PasswordHash;
            Address = user.Address;
            Name = user.Name;
        }

        public User ToUserEntity()
        {
            return new User
            {
                UserId = Id,
                Name = Name,
                Surname = Surname,
                PasswordHash = PasswordHash,
                Email = UserName,
                Address = Address
            };
        }
    }
}