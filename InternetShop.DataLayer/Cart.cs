using System;
using System.Collections.Generic;
using System.Linq;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class Cart
    {
        public ICollection<Tuple<Product, int>> ProductsInCart { get; }

        public Cart()
        {
            ProductsInCart = new List<Tuple<Product, int>>();
        }

        public void AddItem(Product product, int amount)
        {
            var productInCart = ProductsInCart.FirstOrDefault(p => p.Item1.Equals(product));
            if (productInCart == null)
            {
                ProductsInCart.Add(Tuple.Create(product, amount));
            }
            else
            {
                ProductsInCart.Remove(productInCart);
                ProductsInCart.Add(Tuple.Create(product, productInCart.Item2 + amount));
            }
        }

        public void RemoveItem(Product product)
        {
            var productInCart = ProductsInCart.First(t => t.Item1.Equals(product));
            ProductsInCart.Remove(productInCart);
        }

        public decimal ComputeTotalValue()
        {
            return ProductsInCart.Sum(p => p.Item1.Price*p.Item2);
        }

        public void Clear()
        {
            ProductsInCart.Clear();
        }
    }
}