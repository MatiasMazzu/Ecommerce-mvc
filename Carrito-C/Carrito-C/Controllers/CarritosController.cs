using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

        // Agrega 1 CarritoItem a la lista de CarritoItems del User
        [Authorize(Roles = ("Cliente"))]
        public async Task<IActionResult> AgregarAlCarrito(int productoId)
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Producto producto = _context.Productos.FirstOrDefault(p => p.Id == productoId);
            Carrito carrito = _context.Carritos.FirstOrDefault(c => c.ClienteId == userId && c.Activo == true);

            if (producto != null && carrito != null && producto.Activo)
            {
                CarritoItem item = _context.CarritoItems
                    .FirstOrDefault(i => i.ProductoId == productoId && i.CarritoId == carrito.Id);
                if (item != null)
                {
                    item.Cantidad++;
                    _context.CarritoItems.Update(item);
                    _context.Carritos.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    CarritoItem nuevoItem = new CarritoItem()
                    {
                        ProductoId = productoId,
                        Producto = producto,
                        CarritoId = carrito.Id,
                        Carrito = carrito,
                        Cantidad = 1,
                    };

                    _context.CarritoItems.Add(nuevoItem);
                    _context.Carritos.Update(carrito);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(GetCarrito));
        }
        // Muestra el carrito del cliente
        [Authorize(Roles = ("Cliente"))]
        public async Task<IActionResult> GetCarrito()
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            var carritoCliente = _context.CarritoItems
                .Where(c => c.Carrito.ClienteId == userId && c.Carrito.Activo)
                .Include(c => c.Producto);

            return View("GetCarrito", await carritoCliente.ToListAsync());
        }

        [Authorize(Roles = ("Cliente"))]
        // Borra 1 CarritoItem de la lista de CarritoItems del User
        public async Task<IActionResult> RestarDelCarrito(int productoId)
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Producto producto = _context.Productos.FirstOrDefault(p => p.Id == productoId);
            Carrito carrito = _context.Carritos.FirstOrDefault(c => c.ClienteId == userId && c.Activo == true);

            if (producto != null && carrito != null && producto.Activo)
            {
                CarritoItem item = _context.CarritoItems
                    .FirstOrDefault(i => i.ProductoId == productoId && i.CarritoId == carrito.Id);
                if (item != null)
                {
                    item.Cantidad--;
                    if (item.Cantidad > 0)
                    {
                        _context.CarritoItems.Update(item);
                        _context.Carritos.Update(carrito);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.CarritoItems.Remove(item);
                        _context.Carritos.Update(carrito);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    return View("Error");
                }
            }
            return RedirectToAction(nameof(GetCarrito));
        }

        [Authorize(Roles = ("Cliente"))]
        // Elimina todos los productos del carrito
        public async Task<IActionResult> VaciarCarrito()
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Carrito carrito = _context.Carritos.Where(c => c.ClienteId == userId && c.Activo == true)
                .Include(c => c.CarritoItems).FirstOrDefault();

            if (carrito != null)
            {
                List<CarritoItem> carritoItems = carrito.CarritoItems.ToList();
                foreach(var item in carritoItems)
                {
                    _context.CarritoItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
                _context.Carritos.Update(carrito);
                await _context.SaveChangesAsync();
            }
            else
            {
                return View("Error");
            }

            return RedirectToAction(nameof(GetCarrito));

        }
        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.Id == id);
        }
    }
}
