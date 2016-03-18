using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models.AccountModels;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Infrastructure.AccountInfrastructure
{
    public class InternetShopUserStore : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>
    {
        private readonly IUsersRepository usersRepository;

        public IEnumerable<IdentityUser> Users
        {
            get { return usersRepository.Users.Select(u => new IdentityUser(u)); }
        }

        public InternetShopUserStore(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public void Dispose()
        {
            usersRepository.Dispose();
        }

        public async Task CreateAsync(IdentityUser user)
        {
            var entityUser = user.ToUserEntity();
            await usersRepository.CreateUser(entityUser);
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            var entityUser = user.ToUserEntity();
            await usersRepository.UpdateUser(entityUser);
        }

        public async Task DeleteAsync(IdentityUser user)
        {
            var entityUser = user.ToUserEntity();
            await usersRepository.DeleteUser(entityUser);
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return Task.Run(() =>
            {
                var entityUser = usersRepository.Users.FirstOrDefault(u => u.UserId == userId);
                if (entityUser == null)
                    return null;
                return new IdentityUser(entityUser);
            });
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task.Run(() =>
            {
                var entityUser = usersRepository.Users.FirstOrDefault(u => u.Email == userName);
                if (entityUser == null)
                    return null;
                return new IdentityUser(entityUser);
            });
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
        }
    }
}