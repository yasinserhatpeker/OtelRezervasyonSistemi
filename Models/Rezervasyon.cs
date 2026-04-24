using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtelRezervasyon.Models
{
    public class Rezervasyon
    {
        [Key]
        public int RezervasyonId { get; set; }

        [Required(ErrorMessage = "Müşteri seçimi zorunludur")]
        public int MusteriId { get; set; }

        [Required(ErrorMessage = "Oda seçimi zorunludur")]
        public int OdaId { get; set; }

        [Required(ErrorMessage = "Giriş tarihi zorunludur")]
        [DataType(DataType.DateTime)]
        public DateTime GirisTarihi { get; set; }

        [Required(ErrorMessage = "Çıkış tarihi zorunludur")]
        [DataType(DataType.DateTime)]
        public DateTime CikisTarihi { get; set; }

        // Navigation Properties
        [ForeignKey("MusteriId")]
        public virtual Musteri? Musteri { get; set; }

        [ForeignKey("OdaId")]
        public virtual Oda? Oda { get; set; }
    }
}
