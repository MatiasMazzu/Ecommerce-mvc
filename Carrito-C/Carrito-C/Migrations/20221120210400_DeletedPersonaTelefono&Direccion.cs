using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrito_C.Migrations
{
    public partial class DeletedPersonaTelefonoDireccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Direccion_DireccionId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Telefono_TelefonoId",
                table: "Personas");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Telefono");

            migrationBuilder.DropIndex(
                name: "IX_Personas_DireccionId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_TelefonoId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "TelefonoId",
                table: "Personas");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Personas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Personas");

            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TelefonoId",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direccion_Personas_Id",
                        column: x => x.Id,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefono",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefono", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefono_Personas_Id",
                        column: x => x.Id,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personas_DireccionId",
                table: "Personas",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TelefonoId",
                table: "Personas",
                column: "TelefonoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Direccion_DireccionId",
                table: "Personas",
                column: "DireccionId",
                principalTable: "Direccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Telefono_TelefonoId",
                table: "Personas",
                column: "TelefonoId",
                principalTable: "Telefono",
                principalColumn: "Id");
        }
    }
}
