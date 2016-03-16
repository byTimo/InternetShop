using System.Collections.Generic;

namespace InternetShop.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}