using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RecreateOkulMedyalar3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "OkulId",
                table: "Media",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "OkulId",
                table: "Media");
        }
    }
}
