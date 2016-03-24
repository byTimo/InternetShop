using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetShop.DataLayer.Entities;
using InternetShop.DataLayer.Results;

namespace InternetShop.DataLayer.Abstract
{
    public interface IOrderesRepository : IDisposable
    {
        Task<SelectResult<IEnumerable<Order>>> GetAllOrderAsync();

        Task<SelectResult<Order>> GetOrderById(int orderId);

        Task<CreateResult> CreateOrder(User user, IEnumerable<Tuple<Product, int>> products);
    }
}