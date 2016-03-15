using System;
using System.Collections.Generic;
using System.Linq;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class OrdersesRepository : IOrderesRepository
    {
        private readonly InternetShopContext context;

        public OrdersesRepository()
        {
            context = new InternetShopContext();
        }

        public IEnumerable<Order> Orders => context.Orders;

        public IEnumerable<Product> Products => context.Audios
            .Cast<Product>()
            .Concat(context.Videos);

        public void Dispose()
        {
            context.Dispose();
        }
    }
}