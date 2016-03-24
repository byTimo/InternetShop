using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetShop.DataLayer.Entities;
using InternetShop.DataLayer.Results;

namespace InternetShop.DataLayer.Abstract
{
    public interface IProductsRepository : IDisposable
    {
        Task<SelectResult<Product>> GetProductById(int productId);

        Task<SelectResult<IEnumerable<Product>>> GetAllProductAsync();

        Task<CreateResult> CreateProduct(Product product);

        Task<UpdateResult> UpdateProduct(Product product);

        Task<DeleteResult> DeleteProduct(int productId);
    }
}