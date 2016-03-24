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
            get { return usersRepository.GetAllUsers().Result.Result.Select(u => new IdentityUser(u)); }
        }

        public InternetShopUserStore(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
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
            await usersRepository.DeleteUser(entityUser.UserId);
        }

        public async Task<IdentityUser> FindByIdAsync(string userId)
        {
            var dbResult = await usersRepository.GetUserById(userId);
            if(dbResult.IsSucceeded)
                return new IdentityUser(dbResult.Result);
            return null;
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            var dbResult = await usersRepository.GetAllUsers();
            if (dbResult.IsSucceeded)
                return new IdentityUser(dbResult.Result.FirstOrDefault(u => u.Email == userName));
            return null;
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

        public void Dispose()
        {
            //usersRepository.Dispose();
        }
    }
}