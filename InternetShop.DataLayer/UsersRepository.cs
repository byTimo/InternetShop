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
        private readonly InternetShopContext context;

        public UsersRepository()
        {
            context = new InternetShopContext();
        }

        public IEnumerable<User> Users => context.Users;
        public IEnumerable<Role> Roles => context.Roles;

        public async Task CreateUser(User user)
        {
            context.Entry(user).State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            context.Entry(user).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            context.Users.AddOrUpdate(user);
            await context.SaveChangesAsync();
        }

        public async Task CreateRole(Role role)
        {
            context.Entry(role).State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task DeleteRole(Role role)
        {
            context.Entry(role).State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public Task<User> GetUserByIdWithOrders(string userId)
        {
            return context.Users.Include(u => u.Orders).FirstAsync(u => u.UserId.Equals(userId)); 
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}