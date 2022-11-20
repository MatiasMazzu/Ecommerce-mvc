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
    public class DbController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly CarritoCContext _context;

        private List<String> roles = new List<String>() { Configs.AdminRolName, Configs.ClienteRolName, Configs.EmpleadoRolName };
        public DbController(UserManager<Persona> userManager, RoleManager<Rol> roleManager, CarritoCContext context)
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
                Nombre = "Centro",
                Direccion = "Cerrito 1111",
                ImagenArchivo = "shop1.jpg",
                Telefono = "2325-4020",
                Email = "sucursalcentro" + Configs.Email

            };
            Sucursal sucursal2 = new Sucursal()
            {
                Nombre = "Nordelta",
                Direccion = "Av. de los Lagos 2222",
                ImagenArchivo = "shop2.jpg",
                Telefono = "3823-3075",
                Email = "sucursalnordelta" + Configs.Email
            };

            Sucursal sucursal3 = new Sucursal()
            {
                Nombre = "Palermo",
                Direccion = "Av. Santa Fe 3333",
                ImagenArchivo = "shop3.jpg",
                Telefono = "5127-1169",
                Email = "sucursalpalermo" + Configs.Email
            };
            Sucursal sucursal4 = new Sucursal()
            {
                Nombre = "Cañitas",
                Direccion = "Luis María Campos 4444",
                ImagenArchivo = "shop4.jpg",
                Telefono = "5525-3232",
                Email = "sucursalcanitas" + Configs.Email
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
            Producto producto15 = new Producto()
            {
                Nombre = "Footi Yamaha",
                Descripcion = "Zapatillas de running para niñxs",
                PrecioVigente = 12500,
                Activo = true,
                Categoria = ninios,
                ImagenArchivo = "ninios3.1.jpg"
            };
            Producto producto16 = new Producto()
            {
                Nombre = "Footi MvAgusta",
                Descripcion = "Zapatillas de running para niñxs",
                PrecioVigente = 11000,
                Activo = true,
                Categoria = ninios,
                ImagenArchivo = "ninios4.1.png"
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
            _context.Update(producto13);
            _context.Update(producto14);
            _context.Update(producto15);
            _context.Update(producto16);
            await _context.SaveChangesAsync();
        }
        private async Task CrearStockItem()
        {

            StockItem stockItem1 = new StockItem()
            {
                ProductoId = 1,
                Cantidad = 2,
                SucursalId = 1
            };
            StockItem stockItem2 = new StockItem()
            {
                ProductoId = 1,
                Cantidad = 5,
                SucursalId = 2
            };
            StockItem stockItem3 = new StockItem()
            {
                ProductoId = 1,
                Cantidad = 4,
                SucursalId = 3
            };
            StockItem stockItem4 = new StockItem()
            {
                ProductoId = 1,
                Cantidad = 5,
                SucursalId = 4
            };
            StockItem stockItem5 = new StockItem()
            {
                ProductoId = 2,
                Cantidad = 4,
                SucursalId = 1
            };
            StockItem stockItem6 = new StockItem()
            {
                ProductoId = 2,
                Cantidad = 2,
                SucursalId = 2
            };
            StockItem stockItem7 = new StockItem()
            {
                ProductoId = 2,
                Cantidad = 6,
                SucursalId = 3
            };
            StockItem stockItem8 = new StockItem()
            {
                ProductoId = 2,
                Cantidad = 0,
                SucursalId = 4
            };
            StockItem stockItem9 = new StockItem()
            {
                ProductoId = 3,
                Cantidad = 2,
                SucursalId = 1
            };
            StockItem stockItem10 = new StockItem()
            {
                ProductoId = 3,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem11 = new StockItem()
            {
                ProductoId = 3,
                Cantidad = 3,
                SucursalId = 3
            };
            StockItem stockItem12 = new StockItem()
            {
                ProductoId = 3,
                Cantidad = 4,
                SucursalId = 4
            };
            StockItem stockItem13 = new StockItem()
            {
                ProductoId = 4,
                Cantidad = 5,
                SucursalId = 1
            };
            StockItem stockItem14 = new StockItem()
            {
                ProductoId = 4,
                Cantidad = 2,
                SucursalId = 2
            };
            StockItem stockItem15 = new StockItem()
            {
                ProductoId = 4,
                Cantidad = 0,
                SucursalId = 3
            };
            StockItem stockItem16 = new StockItem()
            {
                ProductoId = 4,
                Cantidad = 4,
                SucursalId = 4
            }; StockItem stockItem17 = new StockItem()
            {
                ProductoId = 5,
                Cantidad = 7,
                SucursalId = 1
            };
            StockItem stockItem18 = new StockItem()
            {
                ProductoId = 5,
                Cantidad = 8,
                SucursalId = 2
            };
            StockItem stockItem19 = new StockItem()
            {
                ProductoId = 5,
                Cantidad = 4,
                SucursalId = 3
            }; 
            StockItem stockItem20 = new StockItem()
            {
                ProductoId = 5,
                Cantidad = 0,
                SucursalId = 4
            }; 
            StockItem stockItem21 = new StockItem()
            {
                ProductoId = 6,
                Cantidad = 6,
                SucursalId = 1
            }; 
            StockItem stockItem22 = new StockItem()
            {
                ProductoId = 6,
                Cantidad = 7,
                SucursalId = 2
            };
            StockItem stockItem23 = new StockItem()
            {
                ProductoId = 6,
                Cantidad = 4,
                SucursalId = 3
            };
            StockItem stockItem24 = new StockItem()
            {
                ProductoId = 6,
                Cantidad = 0,
                SucursalId = 4
            };
            StockItem stockItem25 = new StockItem()
            {
                ProductoId = 7,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem26 = new StockItem()
            {
                ProductoId = 7,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem27 = new StockItem()
            {
                ProductoId = 7,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem28 = new StockItem()
            {
                ProductoId = 7,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem29 = new StockItem()
            {
                ProductoId = 8,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem30 = new StockItem()
            {
                ProductoId = 8,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem31 = new StockItem()
            {
                ProductoId = 8,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem32 = new StockItem()
            {
                ProductoId = 8,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem33 = new StockItem()
            {
                ProductoId = 9,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem34 = new StockItem()
            {
                ProductoId = 9,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem35 = new StockItem()
            {
                ProductoId = 9,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem36 = new StockItem()
            {
                ProductoId = 9,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem37 = new StockItem()
            {
                ProductoId = 10,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem38 = new StockItem()
            {
                ProductoId = 10,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem39 = new StockItem()
            {
                ProductoId = 10,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem40 = new StockItem()
            {
                ProductoId = 10,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem41 = new StockItem()
            {
                ProductoId = 11,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem42 = new StockItem()
            {
                ProductoId = 11,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem43 = new StockItem()
            {
                ProductoId = 11,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem44 = new StockItem()
            {
                ProductoId = 11,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem45 = new StockItem()
            {
                ProductoId = 12,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem46 = new StockItem()
            {
                ProductoId = 12,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem47 = new StockItem()
            {
                ProductoId = 12,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem48 = new StockItem()
            {
                ProductoId = 12,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem49 = new StockItem()
            {
                ProductoId = 13,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem50 = new StockItem()
            {
                ProductoId = 13,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem51 = new StockItem()
            {
                ProductoId = 13,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem52 = new StockItem()
            {
                ProductoId = 13,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem53 = new StockItem()
            {
                ProductoId = 14,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem54 = new StockItem()
            {
                ProductoId = 14,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem55 = new StockItem()
            {
                ProductoId = 14,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem56 = new StockItem()
            {
                ProductoId = 14,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem57 = new StockItem()
            {
                ProductoId = 15,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem58 = new StockItem()
            {
                ProductoId = 15,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem59 = new StockItem()
            {
                ProductoId = 15,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem60 = new StockItem()
            {
                ProductoId = 15,
                Cantidad = 6,
                SucursalId = 4
            };
            StockItem stockItem61 = new StockItem()
            {
                ProductoId = 16,
                Cantidad = 6,
                SucursalId = 1
            };
            StockItem stockItem62 = new StockItem()
            {
                ProductoId = 16,
                Cantidad = 0,
                SucursalId = 2
            };
            StockItem stockItem63 = new StockItem()
            {
                ProductoId = 16,
                Cantidad = 5,
                SucursalId = 3
            };
            StockItem stockItem64 = new StockItem()
            {
                ProductoId = 16,
                Cantidad = 6,
                SucursalId = 4
            };

            _context.StockItems.Add(stockItem1);
            _context.StockItems.Add(stockItem2);
            _context.StockItems.Add(stockItem3);
            _context.StockItems.Add(stockItem4);
            _context.StockItems.Add(stockItem5);
            _context.StockItems.Add(stockItem6);
            _context.StockItems.Add(stockItem7);
            _context.StockItems.Add(stockItem8);
            _context.StockItems.Add(stockItem9);
            _context.StockItems.Add(stockItem10);
            _context.StockItems.Add(stockItem11);
            _context.StockItems.Add(stockItem12);
            _context.StockItems.Add(stockItem13);
            _context.StockItems.Add(stockItem14);
            _context.StockItems.Add(stockItem15);
            _context.StockItems.Add(stockItem16);
            _context.StockItems.Add(stockItem17);
            _context.StockItems.Add(stockItem18);
            _context.StockItems.Add(stockItem19);
            _context.StockItems.Add(stockItem20);
            _context.StockItems.Add(stockItem21);
            _context.StockItems.Add(stockItem22);
            _context.StockItems.Add(stockItem23);
            _context.StockItems.Add(stockItem24);
            _context.StockItems.Add(stockItem25);
            _context.StockItems.Add(stockItem26);
            _context.StockItems.Add(stockItem27);
            _context.StockItems.Add(stockItem28);
            _context.StockItems.Add(stockItem29);
            _context.StockItems.Add(stockItem30);
            _context.StockItems.Add(stockItem31);
            _context.StockItems.Add(stockItem32);
            _context.StockItems.Add(stockItem33);
            _context.StockItems.Add(stockItem34);
            _context.StockItems.Add(stockItem35);
            _context.StockItems.Add(stockItem36);
            _context.StockItems.Add(stockItem37);
            _context.StockItems.Add(stockItem38);
            _context.StockItems.Add(stockItem39);
            _context.StockItems.Add(stockItem40);
            _context.StockItems.Add(stockItem41);
            _context.StockItems.Add(stockItem42);
            _context.StockItems.Add(stockItem43);
            _context.StockItems.Add(stockItem44);
            _context.StockItems.Add(stockItem45);
            _context.StockItems.Add(stockItem46);
            _context.StockItems.Add(stockItem47);
            _context.StockItems.Add(stockItem48);
            _context.StockItems.Add(stockItem49);
            _context.StockItems.Add(stockItem50);
            _context.StockItems.Add(stockItem51);
            _context.StockItems.Add(stockItem52);
            _context.StockItems.Add(stockItem53);
            _context.StockItems.Add(stockItem54);
            _context.StockItems.Add(stockItem55);
            _context.StockItems.Add(stockItem56);
            _context.StockItems.Add(stockItem57);
            _context.StockItems.Add(stockItem58);
            _context.StockItems.Add(stockItem59);
            _context.StockItems.Add(stockItem60);
            _context.StockItems.Add(stockItem61);
            _context.StockItems.Add(stockItem62);
            _context.StockItems.Add(stockItem63);
            _context.StockItems.Add(stockItem64);
            await _context.SaveChangesAsync();

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
            Persona admin = new Persona()
            {
                Nombre = "Admin",
                Apellido = "Admin",
                Dni = 11111111,
                UserName = "admin@ort.edu.ar",
                Email = "admin@ort.edu.ar"
            };

            var resultadoAddAdmin = await _userManager.CreateAsync(admin, Configs.PasswordGenerica);

            if (resultadoAddAdmin.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }

        }

        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }
    }
}