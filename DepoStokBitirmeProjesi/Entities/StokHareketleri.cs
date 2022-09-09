using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStokBitirmeProjesi.Entities
{
    [Table("StokHareketleri")]
    public class StokHareketleri
    {

        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Ürün Adeti")]
        public int Adet { get; set; }
        [Required, Display(Name = "Ürün Fiyatı")]
        public decimal Fiyat { get; set; }
        [Display(Name = "Toplam Fiyat")]


        public decimal? ToplamFiyat { get; set; }
        [Required, Display(Name = "Tarih Bilgisi")]


        public DateTime Tarih { get; set; }
        [Required, Display(Name = "İşlem Türü")]

        public int IslemTuru { get; set; }

        public int? DepoId { get; set; }

        [ForeignKey("DepoId")]
        public virtual Depolar? Depo { get; set; }


        public int? UrunId { get; set; }

        [ForeignKey("UrunId")]
        public virtual Urunler? Urun { get; set; }

        public virtual KasaHareketleri? KasaHareketleri { get; set; }
    }
}
