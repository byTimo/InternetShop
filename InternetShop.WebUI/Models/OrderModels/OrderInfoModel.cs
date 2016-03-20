using System;
using System.Collections.Generic;
using System.Linq;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models.OrderModels
{
    public class OrderInfoModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public decimal TotalPrice { get; set; }

        public ICollection<OrderedProduct> OrderedProducts;

        public static OrderInfoModel Create(Order order)
        {
            return new OrderInfoModel
            {
                OrderId = order.OrderId,
                Date = order.Date,
                TotalPrice = order.OrderedProducts.Sum(p => p.Amount*p.Product.Price),
                UserId = order.UserId,
                UserName = order.User.Name,
                UserSurname = order.User.Surname,
                UserEmail = order.User.Email,
                UserAddress = order.User.Address,
                OrderedProducts = order.OrderedProducts
            };
        }
    }
}