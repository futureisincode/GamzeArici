using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStokBitirmeProjesi.Migrations
{
    public partial class vtyukle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UrunId",
                table: "Urunler",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IslemTuru",
                table: "StokHareketleri",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "KasaHareketleriId",
                table: "StokHareketleri",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KasaHareketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlisFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SatisFiyati = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IslemTuru = table.Column<int>(type: "int", nullable: false),
                    UrunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KasaHareketleri", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_UrunId",
                table: "Urunler",
                column: "UrunId");

            migrationBuilder.CreateIndex(
                name: "IX_StokHareketleri_KasaHareketleriId",
                table: "StokHareketleri",
                column: "KasaHareketleriId");

            migrationBuilder.AddForeignKey(
                name: "FK_StokHareketleri_KasaHareketleri_KasaHareketleriId",
                table: "StokHareketleri",
                column: "KasaHareketleriId",
                principalTable: "KasaHareketleri",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_KasaHareketleri_UrunId",
                table: "Urunler",
                column: "UrunId",
                principalTable: "KasaHareketleri",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StokHareketleri_KasaHareketleri_KasaHareketleriId",
                table: "StokHareketleri");

            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_KasaHareketleri_UrunId",
                table: "Urunler");

            migrationBuilder.DropTable(
                name: "KasaHareketleri");

            migrationBuilder.DropIndex(
                name: "IX_Urunler_UrunId",
                table: "Urunler");

            migrationBuilder.DropIndex(
                name: "IX_StokHareketleri_KasaHareketleriId",
                table: "StokHareketleri");

            migrationBuilder.DropColumn(
                name: "UrunId",
                table: "Urunler");

            migrationBuilder.DropColumn(
                name: "KasaHareketleriId",
                table: "StokHareketleri");

            migrationBuilder.AlterColumn<string>(
                name: "IslemTuru",
                table: "StokHareketleri",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
