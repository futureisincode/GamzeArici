using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStokBitirmeProjesi.Migrations
{
    public partial class vtyukle3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_KasaHareketleri_UrunId",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_UrunId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Urunler");

            migrationBuilder.CreateIndex(
                name: "IX_KasaHareketleri_UrunId",
                table: "KasaHareketleri",
                column: "UrunId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KasaHareketleri_Urunler_UrunId",
                table: "KasaHareketleri",
                column: "UrunId",
                principalTable: "Urunler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KasaHareketleri_Urunler_UrunId",
                table: "KasaHareketleri");

            migrationBuilder.DropIndex(
                name: "IX_KasaHareketleri_UrunId",
                table: "KasaHareketleri");

            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Urunler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_UrunId",
                table: "Urunler",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_KasaHareketleri_UrunId",
                table: "Urunler",
                column: "UrunId",
                principalTable: "KasaHareketleri",
                principalColumn: "Id");
        }
    }
}
