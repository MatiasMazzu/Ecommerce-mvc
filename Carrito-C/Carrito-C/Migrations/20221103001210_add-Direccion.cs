using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrito_C.Migrations
{
    public partial class addDireccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Personas",
                newName: "TelefonoId");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Sucursales",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "TelefonoId",
                table: "Sucursales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DireccionId",
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
                name: "IX_Sucursales_TelefonoId",
                table: "Sucursales",
                column: "TelefonoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Telefono_TelefonoId",
                table: "Sucursales",
                column: "TelefonoId",
                principalTable: "Telefono",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Direccion_DireccionId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Telefono_TelefonoId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Telefono_TelefonoId",
                table: "Sucursales");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Telefono");

            migrationBuilder.DropIndex(
                name: "IX_Sucursales_TelefonoId",
                table: "Sucursales");

            migrationBuilder.DropIndex(
                name: "IX_Personas_DireccionId",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_TelefonoId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "TelefonoId",
                table: "Sucursales");

            migrationBuilder.DropColumn(
                name: "DireccionId",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "TelefonoId",
                table: "Personas",
                newName: "Telefono");

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Sucursales",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Telefono",
                table: "Sucursales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Personas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
