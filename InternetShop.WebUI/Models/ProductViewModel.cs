using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Models
{
    public class ProductViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [Display(Name = "Тип продука")]
        [Required(ErrorMessage = "Не выбран тип продукта")]
        public ProductType Type { get; set; }

        [Required(ErrorMessage = "Введите название товара!")]
        [Display(Name = "Название товара")]
        [MinLength(1, ErrorMessage = "Минимаьная длинна названия - 1 символ")]
        public string Name { get; set; }

        [Display(Name = "Описание товара")]
        public string Description { get; set; }

        [Display(Name = "Год выпуска")]
        public int? Year { get; set; }

        [Display(Name = "Цена товара")]
        [Required(ErrorMessage = "Вы не указали цену товара!")]
        public decimal Price { get; set; }

        [Display(Name = "Музыкальный исполнитель")]
        public string Perfomer { get; set; }

        [Display(Name = "Музыкальное направление")]
        public string MusicalDirection { get; set; }

        [Display(Name = "Режисер фильма")]
        public string Director { get; set; }

        [Display(Name = "Жанр фильма")]
        public string Genre { get; set; }

        public IEnumerable<SelectListItem> Types => Enum.GetValues(typeof (ProductType))
            .Cast<ProductType>()
            .Select(t => new SelectListItem
            {
                Text = t.ToString(),
                Value = ((int)t).ToString(),
                Selected = t == Type
            });

        public ProductViewModel()
        {
        }

        public ProductViewModel(Audio audio)
        {
            Type = ProductType.Audio;
            SetCommonProductFields(audio);
            Perfomer = audio.Perfomer;
            MusicalDirection = audio.MusicalDirection;
        }

        private void SetCommonProductFields(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Description = product.Description;
            Year = product.Year;
            Price = product.Price;
        }

        public ProductViewModel(Video video)
        {
            Type = ProductType.Video;
            SetCommonProductFields(video);
            Director = video.Director;
            Genre = video.Genre;
        }

        public static ProductViewModel Create(Product product)
        {
            if (product is Audio)
            {
                return new ProductViewModel(product as Audio);
            }
            else
            {
                return new ProductViewModel(product as Video);
            }
        }

        public Product ToProduct()
        {
            switch (Type)
            {
                case ProductType.Audio:
                    return ToAudio();
                case ProductType.Video:
                    return ToVideo();
                default:
                    throw new ArgumentException();
            }
        }

        private Audio ToAudio()
        {
            return new Audio
            {
                ProductId = ProductId,
                Name = Name,
                Price = Price,
                Description = Description,
                MusicalDirection = MusicalDirection,
                Perfomer = Perfomer,
                Year = Year
            };
        }

        private Video ToVideo()
        {
            return new Video
            {
                ProductId = ProductId,
                Name = Name,
                Price = Price,
                Description = Description,
                Director = Director,
                Genre = Genre,
                Year = Year
            };
        }
    }
}