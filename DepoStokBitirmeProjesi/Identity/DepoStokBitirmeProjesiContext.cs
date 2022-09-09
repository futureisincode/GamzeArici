using DepoStokBitirmeProjesi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepoStokBitirmeProjesi.Identity
{
    public class DepoStokBitirmeProjesiContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, String>
    {
        public DepoStokBitirmeProjesiContext(DbContextOptions<DepoStokBitirmeProjesiContext> options) : base(options)
        {

        }
        public DbSet<Binalar> Binalar { get; set; }
        public DbSet<Depolar> Depolar { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
        public DbSet<StokHareketleri> StokHareketleri { get; set; }
        public DbSet<DepoStokBitirmeProjesi.Entities.KasaHareketleri>? KasaHareketleri { get; set; }

    }
}
