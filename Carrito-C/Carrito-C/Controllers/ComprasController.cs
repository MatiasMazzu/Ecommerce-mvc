using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;

namespace Carrito_C.Controllers
{
    public class ComprasController : Controller
    {
        private readonly CarritoCContext _context;
        private readonly UserManager<Persona> _usermanager;

        public ComprasController(UserManager<Persona> usermanager, CarritoCContext context)
        {
            _context = context;
            _usermanager = usermanager;
        }

        public async Task<IActionResult> RealizarCompra(int carritoId)
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Carrito carrito = _context.Carritos.FirstOrDefault(c => c.Id == carritoId);
            if (carrito != null)
            { 
                    Compra compra = new Compra()
                    {
                        ClienteId = userId,
                        CarritoId = carritoId
                    };
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();

                foreach (CarritoItem carritoItem in _context.CarritoItems.Where(i => i.CarritoId == carritoId))
                {
                    ComprasItem compraItem = new ComprasItem()
                    {
                        
                        CompraId = compra.Id,
                        ProductoId = carritoItem.ProductoId,
                        Cantidad = carritoItem.Cantidad,
                        Subtotal = carritoItem.Subtotal,
                    };
                    _context.ComprasItems.Add(compraItem);
                    _context.CarritoItems.Remove(carritoItem);
                    

                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var carritoCContext = _context.Compras.Include(c => c.Carrito).Include(c => c.Cliente);
            return View(await carritoCContext.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Carrito)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.Carritos, "ClienteId", "ClienteId");
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Apellido");
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,CarritoId,Total,SucursalId")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.Carritos, "ClienteId", "ClienteId", compra.CarritoId);
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Apellido", compra.Id);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.Carritos, "ClienteId", "ClienteId", compra.CarritoId);
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Apellido", compra.Id);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,CarritoId,Total,SucursalId")] Compra compra)
        {
            if (id != compra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.Id))
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
            ViewData["CarritoId"] = new SelectList(_context.Carritos, "ClienteId", "ClienteId", compra.CarritoId);
            ViewData["Id"] = new SelectList(_context.Clientes, "Id", "Apellido", compra.Id);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Carrito)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'CarritoCContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return _context.Compras.Any(e => e.Id == id);
        }
    }
}
