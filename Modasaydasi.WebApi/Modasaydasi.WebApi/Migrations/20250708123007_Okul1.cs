using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modasaydasi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Okul1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_okulMedya_iletisimOkul_OkulmId",
        //        table: "okulMedya");

        //    migrationBuilder.DropIndex(
        //        name: "IX_okulMedya_OkulmId",
        //        table: "okulMedya");
        //
        //
       }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_okulMedya_OkulmId",
                table: "okulMedya",
                column: "OkulmId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_okulMedya_iletisimOkul_OkulmId",
                table: "okulMedya",
                column: "OkulmId",
                principalTable: "iletisimOkul",
                principalColumn: "OkulmId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
