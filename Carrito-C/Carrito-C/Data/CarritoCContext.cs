using Carrito_C.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Carrito_C.Data
{
    public class CarritoCContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>

    {
        public CarritoCContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockItem>().HasKey(ps => new { ps.ProductoId, ps.SucursalId });

            modelBuilder.Entity<StockItem>()
                .HasOne(ps => ps.Producto)
                .WithMany(p => p.ProductoSucursales)
                .HasForeignKey(ps => ps.ProductoId);

            modelBuilder.Entity<StockItem>()
               .HasOne(ps => ps.Sucursal)
               .WithMany(s => s.ProductosSucursal)
               .HasForeignKey(ps => ps.SucursalId);

            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");

            #region Unique
            modelBuilder.Entity<Producto>().HasIndex(p => p.Nombre).IsUnique();
            #endregion

            #region Unique
            modelBuilder.Entity<Cliente>().HasIndex(c => c.CUIT).IsUnique();
            #endregion

            #region Unique
            modelBuilder.Entity<Persona>().HasIndex(per => per.Dni).IsUnique();
            #endregion

            #region Unique
            modelBuilder.Entity<Persona>().HasIndex(per => per.Email).IsUnique();
            #endregion
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
        public DbSet<Rol> Roles { get; set; }

    }
}
