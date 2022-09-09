using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStokBitirmeProjesi.Entities
{
    [Table("Urunler")]

    public class Urunler
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Ürün Adı")]
        public string UrunAdi { get; set; }
        [Display(Name = "Ürün Fotoğrafı")]
        public string? UrunResmi { get; set; }

        [NotMapped, Display(Name = "ResimDosyasi")]
        public IFormFile? ResimDosya { get; set; }
        [Required, Display(Name = "Ürün Stok Adeti")]
        public int UrunStokAdeti { get; set; }
        [Required, Display(Name = "Ürün Alış Fiyatı")]

        public decimal AlisFiyati { get; set; }
        [Required, Display(Name = "Ürün Satış Fiyatı")]

        public decimal SatisFiyati { get; set; }
        [Required, Display(Name = "Ürün Açıklaması")]

        public string Aciklama { get; set; }
        [Required, Display(Name = "Depo Adı")]

        public int DepoId { get; set; }
        [ForeignKey("DepoId")]
        public virtual Depolar Depo { get; set; }

        public List<StokHareketleri>? StokHareketleri { get; set; }

        public virtual KasaHareketleri? KasaHareketleri { get; set; }



    }
}
