using System.Diagnostics;
using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carrito_C.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarritoCContext _context;

        public HomeController(ILogger<HomeController> logger, CarritoCContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Muestra todos los productos
        public IActionResult Index()
        {
            ViewBag.ReturnUrl = HttpContext.Request.Path.ToString();
            var productos = _context.Productos
                .Where(p => p.Activo == true)
                .ToList();
            return View(productos);
        }

        // GET: Muestra las sucursales
        public IActionResult Sucursales()
        {
            var sucursales = _context.Sucursales.ToList();
            return View(sucursales);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}