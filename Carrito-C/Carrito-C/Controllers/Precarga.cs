using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carrito_C.Controllers
{
    public class Precarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly CarritoCContext _context;

        public Precarga(UserManager<Persona> userManager, RoleManager<Rol> roleManager, CarritoCContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public IActionResult Seed()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
