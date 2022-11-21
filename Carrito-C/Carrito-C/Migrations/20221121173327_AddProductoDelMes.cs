using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carrito_C.Migrations
{
    public partial class ProductoProductoDelMes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProductoDelMes",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoDelMes",
                table: "Productos");
        }
    }
}
