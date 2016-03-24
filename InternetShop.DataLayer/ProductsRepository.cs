using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.DataLayer.Results;

namespace InternetShop.DataLayer
{
    public class ProductsRepository : IProductsRepository
    {
        public async Task<SelectResult<Product>> GetProductById(int productId)
        {
            var result = new SelectResult<Product>();
            try
            {
                var getAllResult = await GetAllProductAsync();
                if (getAllResult.IsSucceeded)
                {
                    result.Result = getAllResult.Result.FirstOrDefault(p => p.ProductId == productId);
                    if (result.Result == null)
                        result.AddError("Нет продукта с таким идентификатором");
                }
                foreach (var error in getAllResult.Errors)
                {
                    result.AddError(error);
                }
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public Task<SelectResult<IEnumerable<Product>>> GetAllProductAsync()
        {
            var result = new SelectResult<IEnumerable<Product>>();
            try
            {
                result.Result = InternetShopContext.Instance.Audios
                    .Cast<Product>()
                    .Concat(InternetShopContext.Instance.Videos);
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return Task.FromResult(result);
        }

        public async Task<CreateResult> CreateProduct(Product product)
        {
            var result = new CreateResult();
            try
            {
                InternetShopContext.Instance.Entry(product).State =
                    EntityState.Added;
                await InternetShopContext.Instance.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<UpdateResult> UpdateProduct(Product product)
        {
            var result = new UpdateResult();
            try
            {
                if (product is Audio)
                    InternetShopContext.Instance.Audios.AddOrUpdate((Audio) product);
                else
                    InternetShopContext.Instance.Videos.AddOrUpdate((Video) product);
                await InternetShopContext.Instance.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<DeleteResult> DeleteProduct(int productId)
        {
            var result = new DeleteResult();
            try
            {
                var dbResult = await GetProductById(productId);
                if (dbResult.IsSucceeded)
                {
                    InternetShopContext.Instance.Entry(dbResult.Result).State =
                        EntityState.Deleted;
                    await InternetShopContext.Instance.SaveChangesAsync();
                }
                else
                {
                    foreach (var error in result.Errors)
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