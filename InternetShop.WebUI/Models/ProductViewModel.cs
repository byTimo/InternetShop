using System.Collections.Generic;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}