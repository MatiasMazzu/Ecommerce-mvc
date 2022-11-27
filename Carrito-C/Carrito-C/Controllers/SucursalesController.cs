using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carrito_C.Data;
using Carrito_C.Models;
using Carrito_C.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Carrito_C.Controllers
{
    public class SucursalesController : Controller
    {
        private readonly CarritoCContext _context;

        public SucursalesController(CarritoCContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public IActionResult CrearSucursal()
        {
            return View();
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSucursal([Bind("Id,Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                Sucursal existeSucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.Nombre == sucursal.Nombre);
                if (existeSucursal == null)
                {
                    _context.Sucursales.Add(sucursal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ListarSucursales");
                }
            }
            return View(sucursal);
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> ListarSucursales()
        {
            return View(await _context.Sucursales
                .Include(s => s.ProductosSucursal).ToListAsync());
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> EliminarSucursal(int? id)
        {
            if (id == null)
            {
                return View("Error404");
            }
            bool existeSucursal = await _context.Sucursales.AnyAsync(s => s.Id == id);
            if (!existeSucursal)
            {
                return View("Error404");
            }
            else
            {
                var stockItems = _context.StockItems.Where(s => s.SucursalId == id);
                Sucursal sucursal = _context.Sucursales.Find(id);

                if (stockItems != null)
                {
                    var stock = 0;
                    foreach (var item in stockItems)
                    {
                        stock += item.Cantidad;
                    }
                    if (stock == 0)
                    {
                        foreach (var item in stockItems)
                        {
                            _context.StockItems.Remove(item);
                        }
                        _context.Sucursales.Remove(sucursal);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ListarSucursales");
                    }
                    else
                    {
                        return RedirectToAction("ListarSucursales");
                    }
                }
                else
                {
                    _context.Sucursales.Remove(sucursal);
                }
                return RedirectToAction("ListarSucursales");
            }
        }

    }
}
