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
    
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login viewmodel) 
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(viewmodel.Email, viewmodel.Password, isPersistent: viewmodel.Recordarme, false);
                
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(String.Empty, "Algunos de los datos no es correcto");
            }
                       
            return View(viewmodel);
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
            


    }
}
