using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RecreateOkulMedyalar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.CreateTable(
        name: "OkulMedyalar",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            MedyaId = table.Column<int>(type: "int", nullable: false),
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
    name: "OkulMedyalar");
        }
    }
}
