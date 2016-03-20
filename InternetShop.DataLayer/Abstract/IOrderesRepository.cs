using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IOrderesRepository : IDisposable
    {
        IEnumerable<Order> Orders { get; }

        void CreateOrder(User user, IEnumerable<Tuple<Product, int>> products);
        Task<Order> GetOrderIncludeAllbyId(int orderId);
    }
}