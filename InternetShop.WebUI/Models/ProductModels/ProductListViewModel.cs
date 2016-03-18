using System.Collections.Generic;

namespace InternetShop.WebUI.Models.ProductModels
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}