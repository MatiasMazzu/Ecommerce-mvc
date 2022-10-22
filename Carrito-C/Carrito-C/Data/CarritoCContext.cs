using Carrito_C.Models;
using Microsoft.EntityFrameworkCore;

namespace Carrito_C.Data
{
    public class CarritoCContext : DbContext
    {
        public CarritoCContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Compra>()
              .HasOne(co => co.Cliente)
              .WithMany(c => c.Compras)
              .HasForeignKey(co => co.Id);


            modelBuilder.Entity<StockItem>().HasKey(ps => new { ps.ProductoId, ps.SucursalId });
        }

        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
    }
}
