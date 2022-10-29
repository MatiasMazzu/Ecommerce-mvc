using Carrito_C.Models;
using Carrito_C.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carrito_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _usermanager;
        private readonly SignInManager<Persona> _signInManager;
        public AccountController(UserManager<Persona> usermanager, SignInManager<Persona>signInManager) 
        {
            this._usermanager = usermanager; 
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Email, Password, ConfirmacionPassword")]RegistroUsuario viewmodel)
                    {
            if (!ModelState.IsValid)
            {
                Cliente clienteACrear = new Cliente()
                {
                    Email = viewmodel.Email,
                    UserName = viewmodel.Email
                };

                var resultadoCreate = await _usermanager.CreateAsync(clienteACrear, viewmodel.Password);

                if (resultadoCreate.Succeeded)
                {
                   await _signInManager.SignInAsync(clienteACrear,isPersistent: false );
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in resultadoCreate.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
                return View(viewmodel);
            
        }
    }
}
