using System;
using System.Collections.Generic;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private readonly InternetShopContext context;

        public UsersRepository()
        {
            context = new InternetShopContext();
        }

        public IEnumerable<User> Users => context.Users;
        public IEnumerable<Role> Roles => context.Roles;

        public User CreateUser(User user)
        {
            var addedUser = context.Users.Add(user);
            context.SaveChangesAsync();
            return addedUser;
        }

        public User DeleteUser(User user)
        {
            var deletedUser = context.Users.Remove(user);
            context.SaveChangesAsync();
            return deletedUser;
        }

        public Role CreateRole(Role role)
        {
            var addedRole = context.Roles.Add(role);
            context.SaveChangesAsync();
            return addedRole;
        }

        public Role DeleteRole(Role role)
        {
            var deletedRole = context.Roles.Remove(role);
            context.SaveChangesAsync();
            return deletedRole;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}