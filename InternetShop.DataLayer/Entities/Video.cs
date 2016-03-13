using System.ComponentModel.DataAnnotations;

namespace InternetShop.DataLayer.Entities
{
    public class Video : Product
    {
        [StringLength(50)]
        public string Director { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }
    }
}