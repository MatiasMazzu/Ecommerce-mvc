﻿using Carrito_C.Data;
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
            //CrearStockItem().Wait();

            return RedirectToAction("Index", "Home", new { mensaje = "Proceso Seed finalizado" });
        }

        private async Task CrearSucursales()
        {
            Sucursal sucursal = new Sucursal()
            {
                Nombre = "Sucursal",
                Direccion = "Sucursal 1234",
                Email = "Sucursal"+Configs.Email

            };
            _context.Update(sucursal);
            await _context.SaveChangesAsync();
        }
        private async Task CrearProductos()
        {
            Producto producto = new Producto()
            {
                Nombre = "´Zapatillas",
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
            StockItem producto = new StockItem()
            {
                Producto = _context.Productos.First(),
                Cantidad = 100,
                Sucursal = _context.Sucursales.First()

            };
            _context.Update(producto);
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
            Empleado cliente = new Empleado()
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
