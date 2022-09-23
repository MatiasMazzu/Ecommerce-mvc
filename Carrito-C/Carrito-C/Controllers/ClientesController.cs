using Carrito_C.Data;
using Microsoft.AspNetCore.Mvc;

namespace Carrito_C.Controllers
{
    public class ClientesController : Controller
    {
        private readonly CarritoCContext _context;

        public ClientesController(CarritoCContext context)
        {
            _context = context;
        }
    }
}
