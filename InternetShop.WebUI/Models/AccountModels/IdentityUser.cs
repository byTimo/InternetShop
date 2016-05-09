using System;
using InternetShop.DataLayer.Entities;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Models.AccountModels
{
    public class IdentityUser : User, IUser
    {
        public string Id
        {
            get { return UserId; }
            set { UserId = value; }
        }

        public string UserName
        {
            get { return Email; }
            set { Email = value; }
        }

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
    }
}