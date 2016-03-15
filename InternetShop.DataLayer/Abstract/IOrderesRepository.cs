using System;
using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IOrderesRepository : IDisposable
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<Product> Products { get; }
    }
}