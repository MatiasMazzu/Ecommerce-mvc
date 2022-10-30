using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Carrito_C.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly CarritoCContext _context;

        private List<String> roles = new List<String>() { "Admin", "Cliente", "Empleado", "usuario" };
        public PreCarga(UserManager<Persona> userManager ,RoleManager<Rol> roleManager, CarritoCContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;

        }
        public IActionResult Seed()
        {
            CrearRoles().Wait();
            CrearAdmin().Wait();
            CrearClientes().Wait();
            CrearEmpleados().Wait();


            return RedirectToAction("Index", "Home", new { mensaje = "Proceso Seed finalizado" });
        }

        private async Task CrearEmpleados()
        {
            
        }

        private async Task CrearClientes()
        {
          
        }

        private async Task CrearAdmin()
        {
            
        }

        private async Task CrearRoles()
        {
            foreach(var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }
    }
}
