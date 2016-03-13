using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IUsersRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }

        User CreateUser(User user);
        User DeleteUser(User user);
        Role CreateRole(Role role);
        Role DeleteRole(Role role);


    }
}