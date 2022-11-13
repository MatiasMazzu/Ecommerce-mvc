using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrito_C.Migrations
{
    public partial class CUITUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personas_Dni",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_Email",
                table: "Personas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
