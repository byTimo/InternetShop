using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class ProductViewModel
    {
        private  Product Product { get; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Year { get; set; }

        public decimal Price { get; set; }

        public string Perfomer { get; set; }

        public string MusicalDirection { get; set; }

        public string Director { get; set; }

        public string Genre { get; set; }

        public ProductType Type { get; }

        public ProductViewModel(Product product)
        {
            this.Product = product;
            SetProductFields();
            Type = DefineProductType();
            SetSiblingsFields();
        }

        private void SetProductFields()
        {
            ProductId = Product.ProductId;
            Name = Product.Name;
            Description = Product.Description;
            Year = Product.Year;
            Price = Product.Price;
        }

        private ProductType DefineProductType()
        {
            return Product is Audio ? ProductType.Audio : ProductType.Video;
        }

        private void SetSiblingsFields()
        {
            switch (Type)
            {
                case ProductType.Audio:
                    var audio = Product as Audio;
                    Perfomer = audio.Perfomer;
                    MusicalDirection = audio.MusicalDirection;
                    break;
                case ProductType.Video:
                    var video = Product as Video;
                    Director = video.Director;
                    Genre = video.Genre;
                    break;
            }
        }
    }
}