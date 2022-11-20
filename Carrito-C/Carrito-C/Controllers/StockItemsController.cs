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
        private readonly UserManager<Persona> _usermanager;
        private readonly RoleManager<Rol> _rolManager;
        private readonly CarritoCContext _context;
    
    public StockItemsController(UserManager<Persona> usermanager, RoleManager<Rol> rolManager, CarritoCContext contexto)
    {
        this._usermanager = usermanager;
        this._rolManager = rolManager;
        this._context = contexto;
    }

        // GET: StockItems
        public async Task<IActionResult> Index()
        {
            var carritoCContext = _context.StockItems.Include(s => s.Producto).Include(s => s.Sucursal);
            return View(await carritoCContext.ToListAsync());
        }

        // GET: StockItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockItems == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .Include(s => s.Producto)
                .Include(s => s.Sucursal)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // GET: StockItems/Create
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion");
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion");
            return View();
        }

        // POST: StockItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,SucursalId,Cantidad")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion", stockItem.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion", stockItem.SucursalId);
            return View(stockItem);
        }

        // GET: StockItems/Edit/5
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockItems == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion", stockItem.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion", stockItem.SucursalId);
            return View(stockItem);
        }

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,SucursalId,Cantidad")] StockItem stockItem)
        {
            if (id != stockItem.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockItemExists(stockItem.ProductoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Descripcion", stockItem.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion", stockItem.SucursalId);
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockItems == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .Include(s => s.Producto)
                .Include(s => s.Sucursal)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // POST: StockItems/Delete/5
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockItems == null)
            {
                return Problem("Entity set 'CarritoCContext.StockItems'  is null.");
            }
            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem != null)
            {
                _context.StockItems.Remove(stockItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockItemExists(int id)
        {
          return _context.StockItems.Any(e => e.ProductoId == id);
        }
    }
}
