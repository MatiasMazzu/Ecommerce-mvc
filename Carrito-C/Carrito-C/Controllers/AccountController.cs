using Carrito_C.Data;
using Carrito_C.Models;
using Carrito_C.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace Carrito_C.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _usermanager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _rolManager;
        private readonly CarritoCContext _contexto;
               
        public AccountController(UserManager<Persona> usermanager,
            SignInManager<Persona> signInManager, RoleManager<Rol> rolManager, CarritoCContext contexto)
        {
            this._usermanager = usermanager; 
            this._signInManager = signInManager;
            this._rolManager = rolManager;
            this._contexto = contexto;
        }
        [AllowAnonymous]
        public IActionResult Registrar()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Email, Password, ConfirmacionPassword")]RegistroUsuario viewmodel)
        {
            if (ModelState.IsValid)
            {
                Cliente clienteACrear = new Cliente()
                {
                    Email = viewmodel.Email,
                    UserName = viewmodel.Email
                };

                var resultadoCreate = await _usermanager.CreateAsync(clienteACrear, viewmodel.Password);
                if (resultadoCreate.Succeeded)
                {
                    var resultadoAddRole = await _usermanager.AddToRoleAsync(clienteACrear, Configs.ClienteRolName);
                    if (resultadoCreate.Succeeded)
                    {
                        await _signInManager.SignInAsync(clienteACrear, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, $"no se pudo agregar el rol de {Configs.ClienteRolName}");
                    }
                }
                foreach (var error in resultadoCreate.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(viewmodel);          
        }
        
        private static Cliente GetClienteACrear(RegistroUsuario viewmodel)
        {
            return new Cliente()
            {
                Email = viewmodel.Email,
                UserName = viewmodel.Email
            };
        }
        [AllowAnonymous]
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login viewmodel)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(viewmodel.Email, viewmodel.Password, isPersistent: viewmodel.Recordarme, false);
                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(clienteACrear, isPersistent: false);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListaRoles()
        {
            var roles = _rolManager.Roles.ToList();
            return View(roles);
        }
        public IActionResult AccesoDenegado(String returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public IActionResult TestCurrentUser()
        {
            if (_signInManager.IsSignedIn(User))
            {
                string nombreUsuario = User.Identity.Name;
                //voy al contexto de base de datos
                Persona persona = _contexto.Personas.FirstOrDefault(persona => persona.NormalizedUserName == nombreUsuario.ToUpper());
                // Otra opcion es buscar por ID y no Por el objeto "persona", voy al contexto de base de datos
                int personaId = Int32.Parse(_usermanager.GetUserId(User));
                // Por Claims devuelve un string con los claims
                int personaId2 = Int32.Parse (User.FindFirstValue(ClaimTypes.NameIdentifier));
                //Hace lo mismo que personaId2 sale el claim completo
                var personaId3 = User.Claims.FirstOrDefault(c => c.Type 
                == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            }
            return null;

        }
    }
}
