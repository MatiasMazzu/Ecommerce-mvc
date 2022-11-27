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
    public class ProductosController : Controller
    {
        private readonly CarritoCContext _context;


        public ProductosController(CarritoCContext context)
        {
            _context = context;
        }

        // Lista los productos por categoría
        [AllowAnonymous]
        public async Task<IActionResult> ProductosPorCategoria(int? id)
        {
            if (id != null)
            {
                Categoria categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);

                if (categoria != null)
                {
                    List<Producto> productos = await _context.Productos
                    .Where(p => p.CategoriaId == id)
                    .Include(p => p.Categoria)
                    .ToListAsync();
                    ViewBag.ReturnUrl = HttpContext.Request.Path.ToString();
                    return View(productos);
                }
                else
                {
                    return View("Error404");
                }
            }
            else
            {
                return View("Error404");
            }
        }

        // Retorna la vista para crear un producto
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public IActionResult CrearProducto()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            return View();
        }

        // Crea un producto y lo agrega a la db
        [HttpPost]
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProducto([Bind("Id,Nombre,Descripcion,PrecioVigente,Activo,Destacado,CategoriaId, NombreImagen")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                Producto existeProducto = _context.Productos.FirstOrDefault(p => p.Nombre == producto.Nombre);
                if (existeProducto == null)
                {
                    if (producto.ImagenArchivo == null)
                    {
                        producto.ImagenArchivo = "generica.jpg";
                    }
                    _context.Productos.Add(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ListarProductos");
                }
            }
            return View(producto);
        }

        // Lista todos los productos en la db
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> ListarProductos()
        {
            var productos = this._context.Productos.Include(p => p.Categoria);
            return View(await productos.ToListAsync());
        }

        // Cambia el estado de activo a inactivo, o viceversa
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> CambiarEstado(int? id)
        {
            Producto producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return View("Error404");
            }
            else
            {
                if (producto.Activo)
                {
                    producto.Activo = false;
                }
                else
                {
                    producto.Activo = true;
                }
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListarProductos");
            }
        }

        // Muestra la vista para editar productos
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> Editar(int? id)
        {
            Producto producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return View("Error404");
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descripcion");
            return View(producto);
        }

        // Recibe los parámetros a editar del producto y lo guarda en la db
        [HttpPost]
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int? id, [Bind("Nombre,Descripcion,PrecioVigente,ProductoDelMes,ImagenArchivo, CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                Producto productoAEditar = await _context.Productos.FirstOrDefaultAsync(s => s.Id == id);
                if (productoAEditar == null)
                {
                    return View("Error404");

                }
                else
                {
                    bool existeNombre = await _context.Productos.AnyAsync(p => p.Id != productoAEditar.Id && p.Nombre == producto.Nombre);
                    if (!existeNombre)
                    {
                        productoAEditar.Nombre = producto.Nombre;
                        productoAEditar.Descripcion = producto.Descripcion;
                        productoAEditar.PrecioVigente = producto.PrecioVigente;
                        productoAEditar.ImagenArchivo = producto.ImagenArchivo;
                        productoAEditar.ProductoDelMes = producto.ProductoDelMes;
                        productoAEditar.CategoriaId = producto.CategoriaId;

                        _context.Update(productoAEditar);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ListarProductos");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre");
                    }
                }
            }
            return View(producto);
        }
    }
}
