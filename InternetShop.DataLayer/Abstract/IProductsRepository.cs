using System;
using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IProductsRepository : IDisposable
    {
        IEnumerable<Audio> Audios { get; }
        IEnumerable<Video> Videos { get; }
        IEnumerable<Product> Products { get; }

        Product SaveProduct(Product product);
        Product DeleteProduct(Product product);
    }
}