using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStokBitirmeProjesi.Migrations
{
    public partial class vtYukle5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ToplamFiyat",
                table: "StokHareketleri",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToplamFiyat",
                table: "StokHareketleri");
        }
    }
}
