
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
using Microsoft.Data.SqlClient;

namespace Carrito_C.Controllers
{
    public class PersonasController : Controller
    {
        private readonly CarritoCContext await_context;
        private readonly UserManager<Persona> _userManager;

        public PersonasController(CarritoCContext context,UserManager<Persona>userManager)
        {
            await_context = context;
            this._userManager = userManager;
        }

        // GET: Personas
        public  IActionResult Index()
        {
              return View(await_context.Personas.ToList());
        }

        // GET: Personas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || await_context.Personas == null)
            {
                return NotFound();
            }

            var persona = await_context.Personas
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
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Telefono,Direccion,Email,FechaAlta,UserName,PasswordHash")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                //_context.Personas.Add(persona);

                try
                {
                    await_context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbex)
                {
                    SqlException innerException = dbex.InnerException as SqlException;
                    if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                    {
                        ModelState.AddModelError("Dni", MsgError.DNIExistente );
                        ModelState.AddModelError("Email", MsgError.EmailExistente);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbex.Message);
                    }
                }

            }
            return View(persona);
        
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
                        return Content($"no se ha podido agregar el rol{rolDefinido }");
                    }
                }
                foreach(var error in resultadoNewPersona.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }

            return View(persona);

        }
            
        

        // GET: Personas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || await_context.Personas == null)
            {
                return NotFound();
            }

            var persona = await_context.Personas.Find(id);
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
                    var personaEnDB = await_context.Personas.Find(persona.Id);
                    if (personaEnDB != null)
                    { //Actualizamos
                        personaEnDB.Nombre = persona.Nombre;
                        personaEnDB.Apellido = persona.Apellido;
                        await_context.Personas.Update(personaEnDB);
                        await_context.SaveChanges();
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
            if (id == null || await_context.Personas == null)
            {
                return NotFound();
            }

            var persona = await_context.Personas
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
            if (await_context.Personas == null)
            {
                return Problem("Entity set 'CarritoCContext.Personas'  is null.");
            }
            var persona =  await_context.Personas.Find(id);
            if (persona != null)
            {
                await_context.Personas.Remove(persona);
            }
            
             await_context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return await_context.Personas.Any(e => e.Id == id);
        }
    }
}
