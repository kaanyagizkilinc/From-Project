using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Okul_NameUpdate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciOkulMedyalar",
                table: "KullaniciOkulMedyalar");

            migrationBuilder.RenameTable(
                name: "KullaniciOkulMedyalar",
                newName: "KullaniciOkulBilgileri");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciOkulBilgileri",
                table: "KullaniciOkulBilgileri",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciOkulBilgileri",
                table: "KullaniciOkulBilgileri");

            migrationBuilder.RenameTable(
                name: "KullaniciOkulBilgileri",
                newName: "KullaniciOkulMedyalar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciOkulMedyalar",
                table: "KullaniciOkulMedyalar",
                column: "Id");
        }
    }
}
