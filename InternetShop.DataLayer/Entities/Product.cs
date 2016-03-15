using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.DataLayer.Entities
{
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

        protected bool Equals(Product other)
        {
            return ProductId == other.ProductId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }
}
