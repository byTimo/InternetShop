using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            context = new InternetShopContext();
        }

        public IEnumerable<Audio> Audios => context.Audios;
        public IEnumerable<Video> Videos => context.Videos;

        public IEnumerable<Product> Products => context.Audios.Cast<Product>()
            .Concat(context.Videos);

        public Product SaveProduct(Product product)
        {
            var productType = product is Audio ? ProductType.Audio : ProductType.Video;

            if (product.ProductId == 0)
            {
                CreateNewProduct(product, productType);
            }
            else
            {
                context.Entry(product).State = EntityState.Modified;
            }
            context.SaveChanges();
            return product;
        }

        private void CreateNewProduct(Product product, ProductType productType)
        {
            //switch (productType)
            //{
            //    case ProductType.Audio:
            //        context.Audios.Add(product as Audio);
            //        break;
            //    case ProductType.Video:
            //        context.Videos.Add(product as Video);
            //        break;
            //}
            context.Entry(product).State = EntityState.Added;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}