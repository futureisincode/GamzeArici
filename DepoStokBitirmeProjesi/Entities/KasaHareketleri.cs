using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStokBitirmeProjesi.Entities
{
    [Table("KasaHareketleri")]
    public class KasaHareketleri
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Alış Fiyatı")]
        public decimal?  AlisFiyati { get; set; }
        [ Display(Name = "Satış Fiyatı")]

        public decimal? SatisFiyati { get; set; }
        [Display(Name ="Yapılan Kar")]
        public decimal Kar { get; set; }
        [Required, Display(Name = " işlem Türü")]
        public int IslemTuru { get; set; }
        public int UrunId { get; set; }
        [ForeignKey("UrunId")]
        public virtual Urunler? Urun { get; set; }

        public virtual List<StokHareketleri>? stokHareketleri { get; set; }


    }
}
