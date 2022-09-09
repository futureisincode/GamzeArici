using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStokBitirmeProjesi.Entities
{
    [Table("Depolar")]

    public class Depolar
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Depo Adı")]
        public string DepoAdi { get; set; }
        [Required, Display(Name = "Bina Adı")]
        public int BinaId { get; set; }

        [ForeignKey("BinaId")]
        public virtual Binalar Bina { get; set; }

        public List<StokHareketleri>? StokHareketleri { get; set; }
        

    }
}
