using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.DataLayer.Results;

namespace InternetShop.DataLayer
{
    public class UsersRepository : IUsersRepository
    {
        public Task<SelectResult<IEnumerable<User>>> GetAllUsers()
        {
            var result = new SelectResult<IEnumerable<User>>();
            try
            {
                result.Result = InternetShopContext.Instance.Users;
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return Task.FromResult(result);
        }

        public async Task<SelectResult<User>> GetUserById(string userId)
        {
            var result = new SelectResult<User>();
            try
            {
                result.Result = await InternetShopContext.Instance.Users.FindAsync(userId);
                if(result.Result == null)
                    result.AddError("Нет пользователя с таким идентификатором.");
                
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<SelectResult<User>> GetUserByIdWithOrders(string userId)
        {
            var result = new SelectResult<User>();
            try
            {
                result.Result = await InternetShopContext.Instance.Users.Include(u => u.Orders)
                    .FirstAsync(u => u.UserId.Equals(userId));
                if (result.Result == null)
                    result.AddError("Нет пользователя с таким идентификатором.");
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<CreateResult> CreateUser(User user)
        {
            var result = new CreateResult();
            try
            {
                InternetShopContext.Instance.Entry(user).State = EntityState.Added;
                await InternetShopContext.Instance.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<UpdateResult> UpdateUser(User user)
        {
            var result = new UpdateResult();
            try
            {
                InternetShopContext.Instance.Users.AddOrUpdate(user);
                await InternetShopContext.Instance.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<DeleteResult> DeleteUser(string userId)
        {
            var result = new DeleteResult();
            try
            {
                var dbResult = await GetUserById(userId);
                if (dbResult.IsSucceeded)
                {
                    InternetShopContext.Instance.Entry(dbResult.Result).State =
                        EntityState.Deleted;
                    await InternetShopContext.Instance.SaveChangesAsync();
                }
                else
                {
                    foreach (var error in dbResult.Errors)
                    {
                        result.AddError(error);
                    }
                }
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public void Dispose()
        {
        }
    }
}