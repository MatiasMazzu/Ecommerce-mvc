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
using Microsoft.AspNetCore.Identity;

namespace Carrito_C.Controllers
{
    [Authorize]
    public class StockItemsController : Controller
    {
        private readonly CarritoCContext _context;

        public StockItemsController(CarritoCContext contexto)
        {
            this._context = contexto;
        }
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> ListarStockItems()
        {
            List<StockItem> stockItems = await _context.StockItems
                .Include(s => s.Producto)
                .ThenInclude(p => p.Categoria)
                .Include(s => s.Sucursal)
                .OrderBy(s => s.Sucursal)
                .ToListAsync();
            return View(stockItems);
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> ModificarStock(int itemId, string accion)
        {
            StockItem stockItem = await _context.StockItems.FirstOrDefaultAsync(s => s.Id == itemId);
            if (stockItem != null)
            {
                if (accion == "sumar") stockItem.Cantidad++;
                else if (accion == "restar") stockItem.Cantidad--;
                else return View("Error404");
                _context.StockItems.Update(stockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListarStockItems");
            }
            return RedirectToAction("ListarStockItems");
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> CrearStockItem()
        {
            List<StockItem> stockItems = _context.StockItems.Include(s => s.Producto).ToList();
            List<Producto> productos = await _context.Productos.ToListAsync();
            List<Producto> productosSinStockItems = new List<Producto>();

            foreach (Producto producto in productos)
            {
                if (!stockItems.Any(s => s.ProductoId == producto.Id))
                {
                    productosSinStockItems.Add(producto);
                }
            };
            ViewData["ProductosId"] = new SelectList(productosSinStockItems, "Id", "Nombre");
            ViewData["SucursalesId"] = new SelectList(_context.Sucursales, "Id", "Nombre");

            if (productosSinStockItems.Count() > 0) return View();
            return View("NoHayProductos");
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearStockItem(int sucursalId, int productoId, int cantidad)
        {
            var stockItem = await _context.StockItems.FirstOrDefaultAsync(s => s.ProductoId == productoId && s.SucursalId == sucursalId);
            if (stockItem == null)
            {
                Sucursal sucursal = await _context.Sucursales.FirstOrDefaultAsync(s => s.Id == sucursalId);
                Producto producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == productoId);
                if (sucursal != null && producto != null)
                {
                    StockItem nuevoStockItem = new StockItem()
                    {
                        Producto = producto,
                        ProductoId = productoId,
                        Cantidad = cantidad,
                        Sucursal = sucursal,
                        SucursalId = sucursalId
                    };
                    _context.StockItems.Update(nuevoStockItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ListarStockItems");
                }
                else
                {
                    return View("Error404");
                }
            }
            stockItem.Cantidad = cantidad;
            _context.StockItems.Update(stockItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListarStockItems");
        }

    }
}
