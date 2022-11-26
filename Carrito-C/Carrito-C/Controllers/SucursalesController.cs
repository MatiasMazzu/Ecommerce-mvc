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

        // GET: Sucursales
        public async Task<IActionResult> Index()
        {
              return View(await _context.Sucursales.ToListAsync());
        }

        // GET: Sucursales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }
        // vista para crear una sucursal
        // GET: Sucursales/CrearSucursal
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public IActionResult CrearSucursal()
        {
            return View();
        }

        //Crea una sucursal y lo agrega a la db
        // POST: Sucursales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSucursal([Bind("Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                Sucursal existeSucursal = _context.Sucursales.FirstOrDefault(s => s.Nombre == sucursal.Nombre);
                if (existeSucursal == null)
                {

                    _context.Add(sucursal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(sucursal);
        }

        // Lista todas las sucursales en la db
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> ListarSucursales()
        {
            var sucursales = this._context.Sucursales.Include(s => s.ProductosSucursal);
            return View(await sucursales.ToListAsync());
        }

        // GET: Sucursales/Edit/5
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }
            return View(sucursal);
        }

        // POST: Sucursales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono,Email")] Sucursal sucursal)
        {
            if (id != sucursal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalExists(sucursal.Id))
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
            return View(sucursal);
        }

        // GET: Sucursales/Delete/5
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> DeleteSucursal(int? id)
        {
            if (id == null || _context.Sucursales == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // POST: Sucursales/Delete/5
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [HttpPost, ActionName("DeleteSucursal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sucursales == null)
            {
                return Problem("Entity set 'CarritoCContext.Sucursales'  is null.");
            }
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal != null)
            {
                _context.Sucursales.Remove(sucursal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursalExists(int id)
        {
          return _context.Sucursales.Any(e => e.Id == id);
        }
    }
}
