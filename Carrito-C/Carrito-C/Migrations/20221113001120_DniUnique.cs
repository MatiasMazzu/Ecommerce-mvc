using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrito_C.Migrations
{
    public partial class DniUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personas_CUIT",
                table: "Personas",
                column: "CUIT",
                unique: true,
                filter: "[CUIT] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Dni",
                table: "Personas",
                column: "Dni",
                unique: true,
                filter: "[Dni] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Email",
                table: "Personas",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personas_CUIT",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_Dni",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_Email",
                table: "Personas");
        }
    }
}
