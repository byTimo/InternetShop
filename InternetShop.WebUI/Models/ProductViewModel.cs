using System;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product { get; }

        public int ProductId
        {
            get { return Product.ProductId; }
            set { Product.ProductId = value; }
        }

        public string Name
        {
            get { return Product.Name; }
            set { Product.Name = value; }
        }

        public string Description
        {
            get { return Product.Description; }
            set { Product.Description = value; }
        }

        public int? Year
        {
            get { return Product.Year; }
            set { Product.Year = value; }
        }

        public decimal Price
        {
            get { return Product.Price; }
            set { Product.Price = value; }
        }

        public string Perfomer
        {
            get { return Type == ProductType.Audio ? ((Audio) Product).Perfomer : null; }
            set
            {
                if(Type != ProductType.Audio)
                    throw new ArgumentException("The product is not audio product");
                ((Audio) Product).Perfomer = value;
            }
        }

        public string MusicalDirection
        {
            get { return Type == ProductType.Audio ? ((Audio)Product).MusicalDirection : null; }
            set
            {
                if (Type != ProductType.Audio)
                    throw new ArgumentException("The product is not audio product");
                ((Audio)Product).MusicalDirection = value;
            }
        }

        public string Director
        {
            get { return Type == ProductType.Video ? ((Video)Product).Director : null; }
            set
            {
                if (Type != ProductType.Video)
                    throw new ArgumentException("The product is not video product");
                ((Video)Product).Director = value;
            }
        }

        public string Genre
        {
            get { return Type == ProductType.Video ? ((Video)Product).Genre : null; }
            set
            {
                if (Type != ProductType.Video)
                    throw new ArgumentException("The product is not video product");
                ((Video)Product).Genre = value;
            }
        }

        public ProductType Type { get; }

        public ProductViewModel(Product product)
        {
            Product = product;
            Type = DefineProductType();
        }

        private ProductType DefineProductType()
        {
            return Product is Audio ? ProductType.Audio : ProductType.Video;
        }
    }
}