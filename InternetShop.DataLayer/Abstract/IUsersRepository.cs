using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IUsersRepository : IDisposable
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }

        Task CreateUser(User user);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
        Task CreateRole(Role role);
        Task DeleteRole(Role role);
    }
}