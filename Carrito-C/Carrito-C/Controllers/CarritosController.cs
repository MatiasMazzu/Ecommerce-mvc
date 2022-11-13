using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Carrito_C.Controllers
{
    public class CarritosController : Controller
    {
        private readonly CarritoCContext _context;
        private readonly UserManager<Persona> _usermanager;

        public CarritosController(UserManager<Persona> usermanager, CarritoCContext context)
        {
            _context = context;
            _usermanager = usermanager;
        }
        [Authorize(Roles = ("Cliente"))]
        public async Task<IActionResult> AgregarAlCarrito(int productoId)
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Producto producto = _context.Productos.FirstOrDefault(p => p.Id == productoId);
            StockItem stockItem = _context.StockItems.FirstOrDefault(i => i.ProductoId == productoId);
            Carrito carrito = _context.Carritos.FirstOrDefault(c => c.ClienteId == userId && c.Activo == true);

            if (stockItem != null)
            {
                if (stockItem.Cantidad > 0)
                {
                    if (producto != null && carrito != null && producto.Activo)
                    {
                        CarritoItem item = _context.CarritoItems
                            .FirstOrDefault(i => i.ProductoId == productoId && i.CarritoId == carrito.Id);
                        if (item != null)
                        {
                            item.Cantidad++;
                            stockItem.Cantidad--;
                            _context.CarritoItems.Update(item);
                            _context.StockItems.Update(stockItem);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            CarritoItem nuevoItem = new CarritoItem()
                            {
                                ProductoId = productoId,
                                CarritoId = carrito.Id,
                                Cantidad = 1,
                            };

                            stockItem.Cantidad--;
                            _context.CarritoItems.Add(nuevoItem);
                            _context.StockItems.Update(stockItem);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

            }

            return RedirectToAction("Index");
        }        // GET: Carritos
        public async Task<IActionResult> Index()
        {
            var carritoCContext = _context.Carritos.Include(c => c.Cliente);
            return View(await carritoCContext.ToListAsync());
        }

        // GET: Carritos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // GET: Carritos/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Discriminator");
            return View();
        }

        // POST: Carritos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Activo,Subtotal")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Discriminator", carrito.Id);
            return View(carrito);
        }

        // GET: Carritos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Discriminator", carrito.Id);
            return View(carrito);
        }

        // POST: Carritos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Activo,Subtotal")] Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.Id))
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
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Discriminator", carrito.Id);
            return View(carrito);
        }

        // GET: Carritos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carritos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carritos == null)
            {
                return Problem("Entity set 'CarritoCContext.Carritos'  is null.");
            }
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
          return _context.Carritos.Any(e => e.Id == id);
        }
    }
}
