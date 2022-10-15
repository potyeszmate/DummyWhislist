using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SantaClausHarmadikBeadando.Context;
using SantaClausHarmadikBeadando.Models;

namespace SantaClausHarmadikBeadando.Controllers
{
    public class SantaClausController : Controller
    {
        private readonly EFContext _context;

        public SantaClausController(EFContext context)
        {
            _context = context;
        }

        // GET: SantaClaus
        public async Task<IActionResult> Index()
        {
            // Növekvő sorrendben a prioritás szerint.
            return View(await _context.Wishes.OrderByDescending(p => p.Priority).ToListAsync());
        }

        // GET: SantaClaus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var santaClaus = await _context.Wishes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (santaClaus == null)
            {
                return NotFound();
            }

            return View(santaClaus);
        }

        // GET: SantaClaus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SantaClaus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Colour,Weigth,Priority")] SantaClaus santaClaus)
        {
            if (ModelState.IsValid)
            {
                var checkName = checkIfNameIsAlreadyPresent(santaClaus.Name);

                if (checkName)
                {
                    ModelState.AddModelError("Name", "Ne kívánd ezt többször te kis mohó, mert egyszer sem kapod meg !");
                } 
                else
                {
                    _context.Add(santaClaus);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));     // nameof(index)
                }

                
            }
            return View(santaClaus);
        }

        // Nézzük meg, hogy van-e már ilyen név
        public bool checkIfNameIsAlreadyPresent(string name)
        {
            using (var context = new EFContext())
            {
                var wishes = context.Wishes.ToList();

                foreach (var wish in wishes)
                {
                    if (wish.Name == name)
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        // GET: SantaClaus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var santaClaus = await _context.Wishes.FindAsync(id);
            if (santaClaus == null)
            {
                return NotFound();
            }
            return View(santaClaus);
        }

        // POST: SantaClaus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Colour,Weigth,Priority")] SantaClaus santaClaus)
        {
            if (id != santaClaus.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(santaClaus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SantaClausExists(santaClaus.ID))
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
            return View(santaClaus);
        }

        // GET: SantaClaus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var santaClaus = await _context.Wishes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (santaClaus == null)
            {
                return NotFound();
            }

            return View(santaClaus);
        }

        // POST: SantaClaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var santaClaus = await _context.Wishes.FindAsync(id);
            _context.Wishes.Remove(santaClaus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SantaClausExists(int id)
        {
            return _context.Wishes.Any(e => e.ID == id);
        }
    }
}
