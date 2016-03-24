using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.DataLayer.Entities
{
    public class User
    {
        public string UserId { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(124)]
        public string Name { get; set; }

        [StringLength(124)]
        public string Surname { get; set; }

        [StringLength(256)]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new HashSet<Order>();
        }
    }
}
