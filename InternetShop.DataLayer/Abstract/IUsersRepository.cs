using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetShop.DataLayer.Entities;
using InternetShop.DataLayer.Results;

namespace InternetShop.DataLayer.Abstract
{
    public interface IUsersRepository : IDisposable
    {
        Task<SelectResult<User>> GetUserById(string userId);

        Task<SelectResult<User>> GetUserByIdWithOrders(string userId);

        Task<SelectResult<IEnumerable<User>>> GetAllUsers();

        Task<CreateResult> CreateUser(User user);

        Task<DeleteResult> DeleteUser(string userId);

        Task<UpdateResult> UpdateUser(User user);
    }
}