using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.DataLayer.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Audio> Audios { get; }
        IEnumerable<Video> Videos { get; }
    }
}