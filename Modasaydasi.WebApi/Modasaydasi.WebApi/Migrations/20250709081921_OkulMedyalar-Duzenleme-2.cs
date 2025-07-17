using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class OkulMedyalarDuzenleme2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OkulMedyalar",
                table: "OkulMedyalar");

            migrationBuilder.DropColumn(
            name: "Id",
            table: "OkulMedyalar");


            migrationBuilder.AddColumn<int>(
                name: "MedyaId",
                table: "OkulMedyalar",
                type: "int",
                nullable: false,
                    defaultValue: 0)
    .Annotation("SqlServer:Identity", "1, 1");  // <-- burada identity true

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkulMedyalar",
                table: "OkulMedyalar",
                column: "MedyaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                    name: "PK_OkulMedyalar",
                    table: "OkulMedyalar");

            migrationBuilder.DropColumn(
                name: "MedyaId",
                table: "OkulMedyalar");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OkulMedyalar",
                type: "int",
                nullable: false,
                    defaultValue: 0)
    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OkulMedyalar",
                table: "OkulMedyalar",
                column: "Id");
        }
    }
}
