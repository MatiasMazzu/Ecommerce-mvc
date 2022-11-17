using Carrito_C.Data;
using Carrito_C.Helpers;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Carrito_C.Controllers
{
    public class Precarga : Controller 
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly CarritoCContext _context;

        private List<String> roles = new List<String>() { Configs.AdminRolName, Configs.ClienteRolName, Configs.EmpleadoRolName};
        public Precarga(UserManager<Persona> userManager ,RoleManager<Rol> roleManager, CarritoCContext context)
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
                ImagenArchivo = "shop1.jpg",
                Nombre = "Centro",
                Direccion = "Cerrito 1111",
                Email = "sucursalcentro"+Configs.Email,
                Telefono = "38234020"

            };
            Sucursal sucursal2 = new Sucursal()
            {
                ImagenArchivo = "shop2.jpg",
                Nombre = "Nordelta",
                Direccion = "Av. de los Lagos 2222",
                Email = "sucursalnordelta" + Configs.Email,
                Telefono = "55250032"
            };

            Sucursal sucursal3 = new Sucursal()
            {
                ImagenArchivo = "shop3.jpg",
                Nombre = "Palermo",
                Direccion = "Av. Santa Fe 3333",
                Email = "sucursalpalermo" + Configs.Email,
                Telefono = "23450909"
            };
            Sucursal sucursal4 = new Sucursal()
            {
                ImagenArchivo = "shop4.jpg",
                Nombre = "Cañitas",
                Direccion = "Luis María Campos 4444",
                Email = "sucursalcanitas" + Configs.Email,
                Telefono = "44115169"
            };
            _context.Update(sucursal1);
            _context.Update(sucursal2);
            _context.Update(sucursal3);
            _context.Update(sucursal4);
            await _context.SaveChangesAsync();
        }
       

        private async Task CrearProductos()
        {

            var mujer = _context.Categorias.FirstOrDefault(c => c.Nombre == "Running Mujer");
            var hombre = _context.Categorias.FirstOrDefault(c => c.Nombre == "Running Hombre");
            var ninios = _context.Categorias.FirstOrDefault(c => c.Nombre == "Running Niños");

            Producto producto1 = new Producto()
            {
                Nombre = "Ribuk Air 25",
                Descripcion = "Zapatillas de running para mujer",
                PrecioVigente = 12500,
                Activo = true,
                Categoria = mujer,
                ImagenArchivo = "dama1.1.png"
            };
            Producto producto2 = new Producto()
            {
                Nombre = "Ribuk Run 001",
                Descripcion = "Zapatillas de running para mujer",
                PrecioVigente = 13000,
                Activo = true,
                Categoria = mujer,
                ImagenArchivo = "dama2.1.png"
            };
            Producto producto3 = new Producto()
            {
                Nombre = "Ardidas 1525",
                Descripcion = "Zapatillas de running para mujer",
                PrecioVigente = 11000,
                Activo = true,
                Categoria = mujer,
                ImagenArchivo = "dama3.3.png"
            };
            Producto producto4 = new Producto()
            {
                Nombre = "Ardidas 2022",
                Descripcion = "Zapatillas de running para mujer",
                PrecioVigente = 16000,
                Activo = true,
                Categoria = mujer,
                ImagenArchivo = "dama4.1.jpg"
            };
            Producto producto5 = new Producto()
            {
                Nombre = "Naik RBZ",
                Descripcion = "Zapatillas de running para mujer",
                PrecioVigente = 22000,
                Activo = true,
                Categoria = mujer,
                ImagenArchivo = "dama5.1.png"
            };
            Producto producto6 = new Producto()
            {
                Nombre = "Naik RRR",
                Descripcion = "Zapatillas de running para mujer",
                PrecioVigente = 28000,
                Activo = true,
                Categoria = mujer,
                ImagenArchivo = "dama6.1.png"
            };
            Producto producto7 = new Producto()
            {
                Nombre = "Ribuk Air 30",
                Descripcion = "Zapatillas de running para hombre",
                PrecioVigente = 22000,
                Activo = true,
                Categoria = hombre,
                ImagenArchivo = "hombre1.1.png"
            };
            Producto producto8 = new Producto()
            {
                Nombre = "Ribuk Air Max",
                Descripcion = "Zapatillas de running para hombre",
                PrecioVigente = 21000,
                Activo = true,
                Categoria = hombre,
                ImagenArchivo = "hombre2.1.jpg"
            };
            Producto producto9 = new Producto()
            {
                Nombre = "Ardidas Z1000",
                Descripcion = "Zapatillas de running para hombre",
                PrecioVigente = 19500,
                Activo = true,
                Categoria = hombre,
                ImagenArchivo = "hombre3.1.jpg"
            };
            Producto producto10 = new Producto()
            {
                Nombre = "Ardidas MT09",
                Descripcion = "Zapatillas de running para hombre",
                PrecioVigente = 17500,
                Activo = true,
                Categoria = hombre,
                ImagenArchivo = "hombre4.1.png"
            };
            Producto producto11 = new Producto()
            {
                Nombre = "Naik RC200",
                Descripcion = "Zapatillas de running para hombre",
                PrecioVigente = 21500,
                Activo = true,
                Categoria = hombre,
                ImagenArchivo = "hombre5.1.jpg"
            };
            Producto producto12 = new Producto()
            {
                Nombre = "Naik MONSTER",
                Descripcion = "Zapatillas de running para hombre",
                PrecioVigente = 28500,
                Activo = true,
                Categoria = hombre,
                ImagenArchivo = "hombre6.1.png"
            };
            Producto producto13 = new Producto()
            {
                Nombre = "Footi Repsol",
                Descripcion = "Zapatillas de running para niñxs",
                PrecioVigente = 12500,
                Activo = true,
                Categoria = ninios,
                ImagenArchivo = "ninios1.1.png"
            };
            Producto producto14 = new Producto()
            {
                Nombre = "Footi Honda",
                Descripcion = "Zapatillas de running para niñxs",
                PrecioVigente = 14500,
                Activo = true,
                Categoria = ninios,
                ImagenArchivo = "ninios2.1.png"
            };
            _context.Update(producto1);
            _context.Update(producto2);
            _context.Update(producto3);
            _context.Update(producto4);
            _context.Update(producto5);
            _context.Update(producto6);
            _context.Update(producto7);
            _context.Update(producto8);
            _context.Update(producto9);
            _context.Update(producto10);
            _context.Update(producto11);
            _context.Update(producto12);
            await _context.SaveChangesAsync();
        }
        private async Task CrearStockItem()
        {
            StockItem stockItem = _context.StockItems.FirstOrDefault(i => i.ProductoId == _context.Productos.First().Id && i.SucursalId == _context.Sucursales.First().Id);
            if (stockItem==null)
            {
                stockItem = new StockItem()
                {
                    ProductoId = _context.Productos.First().Id,
                    Cantidad = 100,
                    SucursalId = _context.Sucursales.First().Id

                };
                _context.Add(stockItem);
                await _context.SaveChangesAsync();
            }
        }
        private async Task CrearCategorias()
        {
            Categoria categoria1 = new Categoria()
            {
                Nombre = "Running Mujer",
                Descripcion = "Zapatillas de running p/ mujer"
            };
            if (!_context.Categorias.Any(c => c.Nombre == "Running Mujer"))
            {
                _context.Categorias.Add(categoria1);
                await _context.SaveChangesAsync();
            }
            Categoria categoria2 = new Categoria()
            {
                Nombre = "Running Hombre",
                Descripcion = "Zapatillas de running p/ hombre"
            };
            if (!_context.Categorias.Any(c => c.Nombre == "Running Hombre"))
            {
                _context.Categorias.Add(categoria2);
                await _context.SaveChangesAsync();
            }
            Categoria categoria3 = new Categoria()
            {
                Nombre = "Running Niños",
                Descripcion = "Zapatillas de running p/ niños"
            };
            if (!_context.Categorias.Any(c => c.Nombre == "Running Niños"))
            {
                _context.Categorias.Add(categoria3);
                await _context.SaveChangesAsync();
            }
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
