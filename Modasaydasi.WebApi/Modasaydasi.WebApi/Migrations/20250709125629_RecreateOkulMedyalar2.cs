using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RecreateOkulMedyalar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tip",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tip",
                table: "Media");
        }
    }
}
