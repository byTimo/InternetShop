using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<Product> Products { get; }
    }
}