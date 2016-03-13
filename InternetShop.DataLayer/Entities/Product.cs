using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.DataLayer.Entities
{
    [Table("Products")]
    public abstract class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Year { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }

        protected Product()
        {
            OrderedProducts = new HashSet<OrderedProduct>();
        }
    }
}
