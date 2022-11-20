using Carrito_C.Data;
using Carrito_C.Helpers;
using Carrito_C.Models;
using Carrito_C.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Carrito_C.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _usermanager;
        private readonly SignInManager<Persona> _signInManager;
        private readonly RoleManager<Rol> _rolManager;
        private readonly CarritoCContext _context;
               
        public AccountController(UserManager<Persona> usermanager,
            SignInManager<Persona> signInManager, RoleManager<Rol> rolManager, CarritoCContext contexto)
        {
            this._usermanager = usermanager; 
            this._signInManager = signInManager;
            this._rolManager = rolManager;
            this._context = contexto;
        }
        [Authorize(Roles = Configs.AdminRolName+","+Configs.EmpleadoRolName)]
        public IActionResult RegistrarEmpleado()
        {
            return View();
        }
        [Authorize(Roles = Configs.AdminRolName + "," + Configs.EmpleadoRolName)]
        [HttpPost]
        public async Task<IActionResult> RegistrarEmpleado([Bind("Nombre, Apellido")] RegistroEmpleado viewmodel)
        {
            if (ModelState.IsValid)
            {
                Empleado empleadoACrear = new Empleado()
                {
                    Nombre = viewmodel.Nombre,
                    Apellido = viewmodel.Apellido,
                    UserName = Regex.Replace(viewmodel.Nombre + viewmodel.Apellido + Configs.Email, @"\s+", String.Empty),
                    Email = Regex.Replace(viewmodel.Nombre + viewmodel.Apellido + Configs.Email, @"\s+", String.Empty)
                };
                var resultadoCreate = await _usermanager.CreateAsync(empleadoACrear, Configs.PasswordGenerica);
                if (resultadoCreate.Succeeded)
                {
                    var resultadoAddRole = await _usermanager.AddToRoleAsync(empleadoACrear, Configs.EmpleadoRolName);
                    if (resultadoCreate.Succeeded)
                    {
                        await _signInManager.SignInAsync(empleadoACrear, isPersistent: false);
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
        private async Task<IActionResult> RegistrarRole(Persona user, string rolName, string password)
        {
            var resultadoCreate = await _usermanager.CreateAsync(user, password);
            if (resultadoCreate.Succeeded)
            {
                var resultadoAddRole = await _usermanager.AddToRoleAsync(user, password);
                if (resultadoCreate.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, $"no se pudo agregar el rol de {rolName}");
                }
            }
            foreach (var error in resultadoCreate.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Registrar()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Nombre, Apellido, Dni, Direccion, Telefono, Email, Password, ConfirmacionPassword")] RegistroUsuario viewmodel)
        {
            if (ModelState.IsValid)
            {
                var searchCliente = _context.Clientes.FirstOrDefault(c => c.Dni == viewmodel.Dni);
                var searchEmail = _context.Clientes.FirstOrDefault(c => c.Email == viewmodel.Email);
                
                if(searchEmail == null)
                {
                    if (searchCliente == null)
                    {
                        Cliente clienteACrear = new Cliente()
                        {
                            Email = viewmodel.Email,
                            UserName = viewmodel.Email,
                            Nombre = viewmodel.Nombre,
                            Apellido = viewmodel.Apellido,
                            Dni = viewmodel.Dni,
                            Direccion = viewmodel.Direccion,
                            Telefono = viewmodel.Telefono,
                            CUIT = 2000000000 + viewmodel.Dni,
                            Carritos = new List<Carrito>(),
                            Compras = new List<Compra>()
                        };

                        var resultadoCreate = await _usermanager.CreateAsync(clienteACrear, viewmodel.Password);
                        if (resultadoCreate.Succeeded)
                        {
                            Carrito carrito = new Carrito()
                            {
                                Activo = true,
                                ClienteId = clienteACrear.Id,
                                CarritoItems = new List<CarritoItem>()
                            };
                            clienteACrear.Carritos.Add(carrito);
                            _context.Carritos.Add(carrito);
                            _context.SaveChanges();

                            var resultadoAddRole = await _usermanager.AddToRoleAsync(clienteACrear, Configs.ClienteRolName);
                            if (resultadoAddRole.Succeeded)
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "El DNI ingresado ya está asociado a un cliente");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El email ingresado ya está en uso");
                }


            }
            return View(viewmodel);
        }
        [AllowAnonymous]
        public IActionResult IniciarSesion(int productoId)
        {
            ViewBag.ProductoId = productoId;
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
                    if(viewmodel.productoId > 0)
                    {
                        return RedirectToAction("AgregarAlCarrito", "Carritos", new { productoId = viewmodel.productoId });
                    }else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
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
                Persona persona = _context.Personas.FirstOrDefault(persona => persona.NormalizedUserName == nombreUsuario.ToUpper());
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
