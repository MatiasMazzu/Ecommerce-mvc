using Carrito_C.Data;
using Microsoft.AspNetCore.Mvc;

namespace Carrito_C.Controllers
{
    public class ProductosController : Controller
    {
        private readonly CarritoCContext _context;

        public ProductosController(CarritoCContext context)
        {
            _context = context;
        }
    }
}
