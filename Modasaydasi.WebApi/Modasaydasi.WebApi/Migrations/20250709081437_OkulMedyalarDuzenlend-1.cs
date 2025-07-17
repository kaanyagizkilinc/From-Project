using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class OkulMedyalarDuzenlend1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "iletisimOkul");

            migrationBuilder.DropTable(
                name: "okulMedya");

            migrationBuilder.CreateTable(
                name: "KullaniciOkulMedyalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    Okul = table.Column<int>(type: "int", nullable: false),
                    OkulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MezuniyetYili = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciOkulMedyalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OkulMedyalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OkulMedyalar", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciOkulMedyalar");

            migrationBuilder.DropTable(
                name: "OkulMedyalar");

            migrationBuilder.CreateTable(
                name: "iletisimOkul",
                columns: table => new
                {
                    OkulmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KullaniciId = table.Column<int>(type: "int", nullable: false),
                    MezuniyetYili = table.Column<DateOnly>(type: "date", nullable: false),
                    Okul = table.Column<int>(type: "int", nullable: false),
                    OkulAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iletisimOkul", x => x.OkulmId);
                });

            migrationBuilder.CreateTable(
                name: "okulMedya",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaAdı = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OkulmId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_okulMedya", x => x.Id);
                });
        }
    }
}
