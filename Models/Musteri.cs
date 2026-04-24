using System.ComponentModel.DataAnnotations;

namespace OtelRezervasyon.Models
{
    public class Musteri
    {
        [Key]
        public int MusteriId { get; set; }

        [Required(ErrorMessage = "Ad Soyad zorunludur")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 30 karakter olabilir")]
        public string AdSoyad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon zorunludur")]
        [StringLength(15, ErrorMessage = "Telefon en fazla 15 karakter olabilir")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        public string Telefon { get; set; } = string.Empty;

        [Required(ErrorMessage = "TC kimlik numarası zorunludur")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC kimlik numarası 11 karakter olmalıdır")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "TC kimlik numarası sadece rakamlardan oluşmalıdır")]
        public string TC { get; set; } = string.Empty;
    }
}
