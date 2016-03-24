using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.DataLayer.Results;

namespace InternetShop.DataLayer
{
    public class OrdersRepository : IOrderesRepository
    {
        public Task<SelectResult<IEnumerable<Order>>> GetAllOrderAsync()
        {
            var result = new SelectResult<IEnumerable<Order>>();
            try
            {
                result.Result = InternetShopContext.Instance.Orders;
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return Task.FromResult(result);
        }

        public async Task<SelectResult<Order>> GetOrderById(int orderId)
        {
            var result = new SelectResult<Order>();
            try
            {
                var dbEntity = await InternetShopContext.Instance.Orders.FindAsync(orderId);
                result.Result = dbEntity;
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<CreateResult> CreateOrder(User user, IEnumerable<Tuple<Product, int>> products)
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
            var result = new CreateResult();
            try
            {
                InternetShopContext.Instance.Orders.Add(order);
                await InternetShopContext.Instance.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.AddError(e.Message);
            }
            return result;
        }

        public async Task<SelectResult<Order>> GetOrderIncludeAllbyId(int orderId)
        {
            var result = new SelectResult<Order>();
            try
            {
                result.Result = await InternetShopContext.Instance
                    .Orders
                    .Include("User")
                    .Include("OrderedProducts")
                    .FirstAsync(o => o.OrderId == orderId);
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