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
    public class CategoriasController : Controller
    {
        private readonly CarritoCContext _context;

        public CategoriasController(CarritoCContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public IActionResult CrearCategoria()
        {
            return View();
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCategoria([Bind("Nombre,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var existeCategoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Nombre == categoria.Nombre);
                if (existeCategoria == null)
                {
                    _context.Categorias.Add(categoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ListarCategorias");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Categoria existente.");
                }
            }
            return View(categoria);
        }

        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        public async Task<IActionResult> ListarCategorias()
        {
            return View(await _context.Categorias.ToListAsync());
        }

    }
}
