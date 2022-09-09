using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStokBitirmeProjesi.Migrations
{
    public partial class vtyukle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Kar",
                table: "KasaHareketleri",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kar",
                table: "KasaHareketleri");
        }
    }
}
