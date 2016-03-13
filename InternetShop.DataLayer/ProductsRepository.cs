using System;
using System.Collections.Generic;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer
{
    public class ProductsRepository : IProductsRepository, IDisposable
    {
        private readonly InternetShopContext context;

        public ProductsRepository()
        {
            context = new InternetShopContext();
        }

        public IEnumerable<Audio> Audios => context.Audios;
        public IEnumerable<Video> Videos => context.Videos;

        public void Dispose()
        {
            context.Dispose();
        }
    }
}