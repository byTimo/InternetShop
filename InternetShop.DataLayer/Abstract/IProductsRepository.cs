using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IProductsRepository
    {
        IEnumerable<Audio> Audios { get; }
        IEnumerable<Video> Videos { get; }
        IEnumerable<Product> Products { get; }
    }
}