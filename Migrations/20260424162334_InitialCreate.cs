using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtelRezervasyon.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    MusteriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TC = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.MusteriId);
                });

            migrationBuilder.CreateTable(
                name: "Odalar",
                columns: table => new
                {
                    OdaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdaNumarasi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OdaTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odalar", x => x.OdaId);
                });

            migrationBuilder.CreateTable(
                name: "Rezervasyonlar",
                columns: table => new
                {
                    RezervasyonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriId = table.Column<int>(type: "int", nullable: false),
                    OdaId = table.Column<int>(type: "int", nullable: false),
                    GirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervasyonlar", x => x.RezervasyonId);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervasyonlar_Odalar_OdaId",
                        column: x => x.OdaId,
                        principalTable: "Odalar",
                        principalColumn: "OdaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_MusteriId",
                table: "Rezervasyonlar",
                column: "MusteriId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervasyonlar_OdaId",
                table: "Rezervasyonlar",
                column: "OdaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rezervasyonlar");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Odalar");
        }
    }
}
