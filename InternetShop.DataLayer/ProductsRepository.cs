﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly InternetShopContext context;

        public ProductsRepository()
        {
            context = InternetShopContext.Instance;
        }

        public IEnumerable<Audio> Audios => context.Audios;
        public IEnumerable<Video> Videos => context.Videos;

        public IEnumerable<Product> Products => context.Audios.Cast<Product>()
            .Concat(context.Videos);

        public Product SaveProduct(Product product)
        {
            //context.Entry(product).State = product.ProductId == 0 ? EntityState.Added : EntityState.Modified;
            if (product is Audio)
                context.Audios.AddOrUpdate((Audio) product);
            else
                context.Videos.AddOrUpdate((Video) product);
            context.SaveChanges();
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            context.Entry(product).State = EntityState.Deleted;
            context.SaveChanges();
            return product;
        }

        public void Dispose()
        {
        }
    }
}