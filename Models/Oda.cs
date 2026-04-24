using System.ComponentModel.DataAnnotations;

namespace OtelRezervasyon.Models
{
    public class Oda
    {
        [Key]
        public int OdaId { get; set; }

        [Required(ErrorMessage = "Oda numarası zorunludur")]
        [StringLength(10, ErrorMessage = "Oda numarası en fazla 10 karakter olabilir")]
        public string OdaNumarasi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Oda tipi zorunludur")]
        [StringLength(50, ErrorMessage = "Oda tipi en fazla 50 karakter olabilir")]
        public string OdaTipi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat zorunludur")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Fiyat { get; set; }
    }
}
