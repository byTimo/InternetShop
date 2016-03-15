using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class ProductViewModel
    {
        private Product product;

        [HiddenInput(DisplayValue = false)]
        public int ProductId
        {
            get { return product.ProductId; }
            set { product.ProductId = value; }
        }

        [Required(ErrorMessage = "Введите название товара!")]
        [Display(Name = "Название товара")]
        [MinLength(1, ErrorMessage = "Минимаьная длинна названия - 1 символ")]
        public string Name
        {
            get { return product.Name; }
            set { product.Name = value; }
        }

        [Display(Name = "Описание товара")]
        public string Description
        {
            get { return product.Description; }
            set { product.Description = value; }
        }

        [Display(Name = "Год выпуска")]
        public int? Year
        {
            get { return product.Year; }
            set { product.Year = value; }
        }

        [Display(Name = "Цена товара")]
        [Required(ErrorMessage = "Вы не указали цену товара!")]
        public decimal Price
        {
            get { return product.Price; }
            set { product.Price = value; }
        }

        [Display(Name = "Музыкальный исполнитель")]
        public string Perfomer
        {
            get { return Type == ProductType.Audio ? ((Audio) product).Perfomer : null; }
            set
            {
                if(Type != ProductType.Audio)
                    throw new ArgumentException("The product is not audio product");
                ((Audio) product).Perfomer = value;
            }
        }

        [Display(Name = "Музыкальное направление")]
        public string MusicalDirection
        {
            get { return Type == ProductType.Audio ? ((Audio)product).MusicalDirection : null; }
            set
            {
                if (Type != ProductType.Audio)
                    throw new ArgumentException("The product is not audio product");
                ((Audio)product).MusicalDirection = value;
            }
        }

        [Display(Name = "Режисер фильма")]
        public string Director
        {
            get { return Type == ProductType.Video ? ((Video)product).Director : null; }
            set
            {
                if (Type != ProductType.Video)
                    throw new ArgumentException("The product is not video product");
                ((Video)product).Director = value;
            }
        }

        [Display(Name = "Жанр фильма")]
        public string Genre
        {
            get { return Type == ProductType.Video ? ((Video)product).Genre : null; }
            set
            {
                if (Type != ProductType.Video)
                    throw new ArgumentException("The product is not video product");
                ((Video)product).Genre = value;
            }
        }

        public ProductType Type { get; }

        public ProductViewModel(Product product)
        {
            this.product = product;
            Type = DefineProductType();
        }

        public ProductViewModel() : this(new Audio()) { }

        private ProductType DefineProductType()
        {
            return product is Audio ? ProductType.Audio : ProductType.Video;
        }

        public Product ToProduct()
        {
            return product;
        }
    }
}