using System;
using InternetShop.DataLayer.Entities;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Models.AccountModels
{
    public class IdentityUser : IUser
    {
        public string Id { get; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }


        public IdentityUser(string email)
        {
            Id = Guid.NewGuid().ToString();
            UserName = email;
        }

        public IdentityUser(User user)
        {
            Id = user.UserId ?? Guid.NewGuid().ToString();
            UserName = user.Email;
            PasswordHash = user.PasswordHash;
            Name = user.Name;
            Surname = user.Surname;
            Address = user.Address;
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