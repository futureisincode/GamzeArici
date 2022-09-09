using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepoStokBitirmeProjesi.Entities
{
    [Table("Binalar")]
    public class Binalar
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Bina Adı")]
        public string BinaAdi { get; set; }
        [Required, Display(Name = "Şehir")]
        public string Sehir { get; set; }

        public virtual List<Depolar> depolar { get; set; }
    }
}
