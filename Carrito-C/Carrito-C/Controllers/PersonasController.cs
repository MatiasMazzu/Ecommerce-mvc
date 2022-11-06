
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.AspNetCore.Identity;
using Carrito_C.Helpers;

namespace Carrito_C.Controllers
{
    public class PersonasController : Controller
    {
        private readonly CarritoCContext _context;
        private readonly UserManager<Persona> _userManager;

        public PersonasController(CarritoCContext context,UserManager<Persona>userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: Personas
        public  IActionResult Index()
        {
              return View(_context.Personas.ToList());
        }

        // GET: Personas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = _context.Personas
                .FirstOrDefault(p => p.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Telefono,Direccion,Email,FechaAlta,UserName,PasswordHash")] Persona persona){
            if (ModelState.IsValid)
            {
                //_context.Personas.Add(persona);
                //_context.SaveChanges();

                persona.UserName = persona.Email;

                var resultadoNewPersona = await _userManager.CreateAsync(persona, Configs.PasswordGenerica);
                if (resultadoNewPersona.Succeeded)
                {
                    IdentityResult resultadoAddRole;
                    String rolDefinido;

                        rolDefinido = Configs.ClienteRolName;

                    resultadoAddRole = await _userManager.AddToRoleAsync(persona, rolDefinido);

                    if (resultadoAddRole.Succeeded)
                    {
                        return RedirectToAction("Index", "Personas");
                    }
                    else
                    {
                        return Content($"no se a podido agregar el rol{rolDefinido }");
                    }
                }
                foreach(var error in resultadoNewPersona.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }


                
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = _context.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Telefono,Direccion,Email,FechaAlta,UserName,PasswordHash")] Persona persona)
        {
            if (id != persona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var personaEnDB = _context.Personas.Find(persona.Id);
                    if (personaEnDB != null)
                    { //Actualizamos
                        personaEnDB.Nombre = persona.Nombre;
                        personaEnDB.Apellido = persona.Apellido;
                        _context.Personas.Update(personaEnDB);
                        _context.SaveChanges();
                    }
                    else {
                        return NotFound();
                    }
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = _context.Personas
                .FirstOrDefault(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'CarritoCContext.Personas'  is null.");
            }
            var persona =  _context.Personas.Find(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }
            
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return _context.Personas.Any(e => e.Id == id);
        }
    }
}
