using System.ComponentModel.DataAnnotations;

namespace InternetShop.DataLayer.Entities
{
    public class Audio : Product
    {
        [StringLength(50)]
        public string Perfomer { get; set; }

        [StringLength(50)]
        public string MusicalDirection { get; set; }
    }
}