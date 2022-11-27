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
using Carrito_C.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Carrito_C.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly CarritoCContext _context;
        private readonly UserManager<Persona> _usermanager;

        public ClientesController(UserManager<Persona> usermanager, CarritoCContext context)
        {
            _context = context;
            _usermanager = usermanager;
        }

        [Authorize(Roles = Configs.ClienteRolName)]
        public async Task<IActionResult> Perfil()
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Cliente cliente = await _context.Clientes.FindAsync(userId);

            if (cliente != null)
            {
                return View(cliente);
            }
            return View("Error404");
        }

        [Authorize(Roles = Configs.ClienteRolName)]
        public async Task<IActionResult> EditarPerfil()
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Cliente cliente = await _context.Clientes.FindAsync(userId);

            if (cliente != null)
            {
                return View(cliente);
            }
            return View("Error404");
        }

        [Authorize(Roles = Configs.ClienteRolName)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil([Bind("Id,Telefono,Direccion")] Cliente clienteEditado)
        {
            int userId = Int32.Parse(_usermanager.GetUserId(User));
            Cliente cliente = await _context.Clientes.FindAsync(userId);
            cliente.Direccion = clienteEditado.Direccion;
            cliente.Telefono = clienteEditado.Telefono;
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Perfil");
        }
    }
}
