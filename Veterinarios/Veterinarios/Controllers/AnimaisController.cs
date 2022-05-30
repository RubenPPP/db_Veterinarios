using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Veterinarios.Data;
using Vets.Models;

namespace Veterinarios.Controllers
{
    public class AnimaisController : Controller
    {
        /// <summary>
        /// manipula os dados da base de dados
        /// </summary>
        private readonly ApplicationDbContext _context;
        /// <summary>
        /// manipula os dados dos utilizadores
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public AnimaisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            //_userManager = userManager;
        }

        // GET: Animais
        public async Task<IActionResult> Index()
        {
            var listaDeAnimais = _context.Animais.Include(a => a.Dono).OrderBy(a => a.Nome);

            if (User.IsInRole("Veterinario"))
            {
                return View(await listaDeAnimais.ToListAsync());
            }

            // var auxiliar
            string idUserAutenticado = _userManager.GetUserId(User);

            // Vamos restringir os dados de todos os 'animais' à pessoa que está autenticada (cliente)
            listaDeAnimais = (IOrderedQueryable<Animais>)listaDeAnimais.Where(a => a.Dono.UserID == idUserAutenticado);

            return View(await listaDeAnimais.ToListAsync());
        }

        // GET: Animais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var animais = await _context.Animais
                .Include(a => a.Dono)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (User.IsInRole("Veterinario"))
            {
                return View(animais);
            }

            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            // var auxiliar
            string idUserAutenticado = _userManager.GetUserId(User);

            animais = await _context.Animais
                .Include(a => a.Dono)
                .Where(a => a.Id == id && a.Dono.UserID == idUserAutenticado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animais == null)
            {
                return RedirectToAction("Index");
            }

            return View(animais);
        }

        // GET: Animais/Create
        public IActionResult Create()
        {
            // Como não se quer que o utilizador escolha o 'dono' de uma lista, esta pesquisa é totalmente inútil 
            // ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF");
            return View();
        }

        // POST: Animais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Raca,Especie,DataNascimento,Peso")] Animais animal)
        {
            // Nos dados do novo Animal, falta a  FK para o seu 'dono', como obtê-lo?
            // var auxiliar
            string idUserAutenticado = _userManager.GetUserId(User);

            int fkDono = _context.Donos.Where(d => d.UserID == idUserAutenticado).FirstOrDefault().Id;
            animal.DonoFK = fkDono;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch (Exception e)
                {
                    
                }
            }
            // Por igual razão, esta pesquisa também não vai ser usada
            // ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
            return View(animal);
        }

        // GET: Animais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }
            string idUserAutenticado = _userManager.GetUserId(User);
            var animais = await _context.Animais.FindAsync(id);
            if (animais == null)
            {
                return RedirectToAction("Index");
            }
            ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
            return View(animais);
        }

        // POST: Animais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Raca,Especie,DataNascimento,Peso,Fotografia,DonoFK")] Animais animais)
        {
            if (id != animais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimaisExists(animais.Id))
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
            ViewData["DonoFK"] = new SelectList(_context.Donos, "Id", "NIF", animais.DonoFK);
            return View(animais);
        }

        // GET: Animais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animais == null)
            {
                return NotFound();
            }

            var animais = await _context.Animais
                .Include(a => a.Dono)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animais == null)
            {
                return NotFound();
            }

            return View(animais);
        }

        // POST: Animais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animais == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Animais'  is null.");
            }
            var animais = await _context.Animais.FindAsync(id);
            if (animais != null)
            {
                _context.Animais.Remove(animais);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimaisExists(int id)
        {
          return (_context.Animais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
