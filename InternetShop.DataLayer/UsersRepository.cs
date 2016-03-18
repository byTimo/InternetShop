using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class UsersRepository : IUsersRepository
    {
        public IEnumerable<User> Users => InternetShopContext.Instance.Users;
        public IEnumerable<Role> Roles => InternetShopContext.Instance.Roles;

        public async Task CreateUser(User user)
        {
            InternetShopContext.Instance.Entry(user).State = EntityState.Added;
            await InternetShopContext.Instance.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            InternetShopContext.Instance.Entry(user).State = EntityState.Deleted;
            await InternetShopContext.Instance.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            InternetShopContext.Instance.Users.AddOrUpdate(user);
            await InternetShopContext.Instance.SaveChangesAsync();
        }

        public async Task CreateRole(Role role)
        {
            InternetShopContext.Instance.Entry(role).State = EntityState.Added;
            await InternetShopContext.Instance.SaveChangesAsync();
        }

        public async Task DeleteRole(Role role)
        {
            InternetShopContext.Instance.Entry(role).State = EntityState.Added;
            await InternetShopContext.Instance.SaveChangesAsync();
        }

        public Task<User> GetUserById(string userId)
        {
            return InternetShopContext.Instance.Users.FindAsync(userId);
        }

        public Task<User> GetUserByIdWithOrders(string userId)
        {
            return InternetShopContext.Instance.Users.Include(u => u.UserId).FirstAsync(u => u.UserId.Equals(userId)); 
        }

        public void Dispose()
        {
        }
    }
}