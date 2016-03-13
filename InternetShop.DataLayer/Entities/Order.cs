using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.DataLayer.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }

        public virtual User User { get; set; }

        public Order()
        {
            OrderedProducts = new HashSet<OrderedProduct>();
        }
    }
}
