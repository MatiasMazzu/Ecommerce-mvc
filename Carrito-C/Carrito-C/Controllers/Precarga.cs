using Carrito_C.Data;
using Carrito_C.Helpers;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Carrito_C.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly CarritoCContext _context;

        private List<String> roles = new List<String>() { Configs.AdminRolName, Configs.ClienteRolName, Configs.EmpleadoRolName};
        public PreCarga(UserManager<Persona> userManager ,RoleManager<Rol> roleManager, CarritoCContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;

        }
        public IActionResult Seed()
        {
            CrearRoles().Wait();
            CrearAdmin().Wait();
            CrearClientes().Wait();
            CrearEmpleados().Wait();
            CrearSucursales().Wait();
            CrearCategorias().Wait();
            CrearProductos().Wait();
            CrearStockItem().Wait();

            return RedirectToAction("Index", "Home", new { mensaje = "Proceso Seed finalizado" });
        }

        private async Task CrearSucursales()
        {
            Sucursal sucursal1 = new Sucursal()
            {
                Nombre = "SucursalCentro",
                Direccion = "Cerrito 1111",
                Email = "SucursalCentro"+Configs.Email

            };
            Sucursal sucursal2 = new Sucursal()
            {
                Nombre = "SucursalNordelta",
                Direccion = "Av. de los Lagos 2222",
                Email = "SucursalNordelta" + Configs.Email
            };

            Sucursal sucursal3 = new Sucursal()
            {
                Nombre = "SucursalPalermo",
                Direccion = "Av. Santa Fe 3333",
                Email = "SucursalPalermo" + Configs.Email
            };
            Sucursal sucursal4 = new Sucursal()
            {
                Nombre = "SucursalCañitas",
                Direccion = "Luis María Campos 4444",
                Email = "SucursalCanitas" + Configs.Email
            };
            Sucursal sucursal5 = new Sucursal()
            {
                Nombre = "SucursalLaPlata",
                Direccion = "Diagonal 73 5555",
                Email = "SucursalLaPlata" + Configs.Email
            };
            _context.Update(sucursal1);
            _context.Update(sucursal2);
            _context.Update(sucursal3);
            _context.Update(sucursal4);
            _context.Update(sucursal5);
            await _context.SaveChangesAsync();
        }
       

        private async Task CrearProductos()
        {
            Producto producto = new Producto()
            {
                Nombre = "Zapatillas",
                Descripcion = "Zapatillas",
                PrecioVigente = 100,
                Activo = true,
                Categoria = _context.Categorias.First()

        };
            _context.Update(producto);
            await _context.SaveChangesAsync();
        }
        private async Task CrearStockItem()
        {
            StockItem stockItem = new StockItem()
            {                
                ProductoId = _context.Productos.First().Id,
                Cantidad = 100,                
                SucursalId = _context.Sucursales.First().Id

            };
            _context.Add(stockItem);
            await _context.SaveChangesAsync();
        }
        private async Task CrearCategorias()
        {
            Categoria categoria = new Categoria()
            {
                Nombre = "Calzado",
                Descripcion = "Calzado"
            };
            _context.Update(categoria);
            await _context.SaveChangesAsync();
        }
        private async Task CrearEmpleados()
        {
            Empleado empleado = new Empleado()
            {
                Nombre = "Empleado",
                Apellido = "Empleado",
                Dni = 11111112,
                UserName = "Empleado@ort.edu.ar",
                Email = "Empleado@ort.edu.ar"
            };

            var resultadoAddAdmin = await _userManager.CreateAsync(empleado, Configs.PasswordGenerica);

            if (resultadoAddAdmin.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado, Configs.EmpleadoRolName);
            }
        }

        private async Task CrearClientes()
        {
            Cliente cliente = new Cliente()
            {
                Nombre = "Cliente",
                Apellido = "Cliente",
                Dni = 11111113,
                UserName = "Cliente@ort.edu.ar",
                Email = "Cliente@ort.edu.ar"
            };

            var resultadoAddAdmin = await _userManager.CreateAsync(cliente, Configs.PasswordGenerica);

            if (resultadoAddAdmin.Succeeded)
            {
                Carrito carrito = new Carrito() { ClienteId = cliente.Id, Activo = true };
                _context.Carritos.Add(carrito);
                _context.SaveChanges();
                await _userManager.AddToRoleAsync(cliente, Configs.ClienteRolName);
            }
        }

        private async Task CrearAdmin()
        {
            Persona admin = new Persona() { 
                Nombre = "Admin",
                Apellido = "Admin",
                Dni = 11111111,
                UserName = "admin@ort.edu.ar",
                Email = "admin@ort.edu.ar"                 
            };

            var resultadoAddAdmin = await _userManager.CreateAsync(admin,Configs.PasswordGenerica);

            if (resultadoAddAdmin.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin,"Admin");
            }

        }

        private async Task CrearRoles()
        {
            foreach(var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }
    }
}
