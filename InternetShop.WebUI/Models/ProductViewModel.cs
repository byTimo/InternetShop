using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class ProductViewModel
    {
        
        public Product Product { get; }

        [HiddenInput(DisplayValue = false)]
        public int ProductId
        {
            get { return Product.ProductId; }
            set { Product.ProductId = value; }
        }

        [Required(ErrorMessage = "Введите название товара!")]
        [Display(Name = "Название товара")]
        [MinLength(1, ErrorMessage = "Минимаьная длинна названия - 1 символ")]
        public string Name
        {
            get { return Product.Name; }
            set { Product.Name = value; }
        }

        [Display(Name = "Описание товара")]
        public string Description
        {
            get { return Product.Description; }
            set { Product.Description = value; }
        }

        [Display(Name = "Год выпуска")]
        public int? Year
        {
            get { return Product.Year; }
            set { Product.Year = value; }
        }

        [Display(Name = "Цена товара")]
        [Required(ErrorMessage = "Вы не указали цену товара!")]
        public decimal Price
        {
            get { return Product.Price; }
            set { Product.Price = value; }
        }

        [Display(Name = "Музыкальный исполнитель")]
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

        [Display(Name = "Музыкальное направление")]
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

        [Display(Name = "Режисер фильма")]
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

        [Display(Name = "Жанр фильма")]
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