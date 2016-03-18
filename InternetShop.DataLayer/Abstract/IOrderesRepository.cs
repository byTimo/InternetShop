using System;
using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IOrderesRepository : IDisposable
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<Product> Products { get; }

        void CreateOrder(User user, IEnumerable<Tuple<Product, int>> products);
    }
}