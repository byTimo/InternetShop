using System.Linq;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models.AccountModels;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Infrastructure.AccountInfrastructure
{
    public class InternetShopUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
    {
        private readonly IUsersRepository usersRepository;

        public InternetShopUserStore(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public void Dispose()
        {
            usersRepository.Dispose();
        }

        public async Task CreateAsync(ApplicationUser user)
        {
            var entityUser = user.ToUserEntity();
            await usersRepository.CreateUser(entityUser);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            var entityUser = user.ToUserEntity();
            await usersRepository.UpdateUser(entityUser);
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            var entityUser = user.ToUserEntity();
            await usersRepository.DeleteUser(entityUser);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return Task.Run(() =>
            {
                var entityUser = usersRepository.Users.FirstOrDefault(u => u.UserId == userId);
                if (entityUser == null)
                    return null;
                return new ApplicationUser(entityUser);
            });
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task.Run(() =>
            {
                var entityUser = usersRepository.Users.FirstOrDefault(u => u.Name == userName);
                if (entityUser == null)
                    return null;
                return new ApplicationUser(entityUser);
            });
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            return Task.FromResult(string.IsNullOrEmpty(user.PasswordHash));
        }
    }
}