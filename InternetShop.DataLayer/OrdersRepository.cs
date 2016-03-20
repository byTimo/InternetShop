using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class OrdersRepository : IOrderesRepository
    {
        private InternetShopContext context;

        public OrdersRepository()
        {
            context = InternetShopContext.Instance;
        }

        public IEnumerable<Order> Orders => context.Orders;
        public IEnumerable<Product> Products => null;

        public void CreateOrder(User user, IEnumerable<Tuple<Product, int>> products)
        {
            var order = new Order
            {
                Date = DateTime.Now,
                User = user,
                UserId = user.UserId
            };
            order.OrderedProducts = products
                .Select(t => new OrderedProduct(t.Item1, order, t.Item2))
                .ToList();
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public Task<Order> GetOrderIncludeAllbyId(int orderId)
        {
            return context.Orders
                .Include("User")
                .Include("OrderedProducts")
                .FirstAsync(o => o.OrderId == orderId);
        }

        public void Dispose()
        {
        }
    }
}