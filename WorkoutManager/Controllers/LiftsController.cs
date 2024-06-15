using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutManager.Data;
using WorkoutManager.Models;

namespace WorkoutManager.Controllers
{
    public class LiftsController : Controller
    {
        private readonly WorkoutManagerContext _context;

        public LiftsController(WorkoutManagerContext context)
        {
            _context = context;
        }

        // GET: Lifts
        public async Task<IActionResult> Index(string liftType, string searchString)
        {
            if(_context.Lift == null)
            {
                return Problem("Entity set 'WorkoutManagerContext.Lift' is null");
            }

            IQueryable<string> typeQuery = from l in _context.Lift
                                           orderby l.Type
                                           select l.Type;

            var lifts = from l in _context.Lift
                        select l;

            if(!String.IsNullOrEmpty(searchString))
            {
                lifts = lifts.Where(s => s.Name!.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(liftType))
            {
                lifts = lifts.Where(x => x.Type == liftType);
            }

            var liftTypeVM = new LiftTypeViewModel
            {
                Types = new SelectList(await typeQuery.Distinct().ToListAsync()),
                Lifts = await lifts.ToListAsync()
            };

            return View(liftTypeVM);
        }

        // GET: Lifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lift = await _context.Lift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lift == null)
            {
                return NotFound();
            }

            return View(lift);
        }

        // GET: Lifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Weight,Reps,Type,Notes,RPE,MainBodyPart")] Lift lift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lift);
        }

        // GET: Lifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lift = await _context.Lift.FindAsync(id);
            if (lift == null)
            {
                return NotFound();
            }
            return View(lift);
        }

        // POST: Lifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Weight,Reps,Type,Notes,RPE,MainBodyPart")] Lift lift)
        {
            if (id != lift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LiftExists(lift.Id))
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
            return View(lift);
        }

        // GET: Lifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lift = await _context.Lift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lift == null)
            {
                return NotFound();
            }

            return View(lift);
        }

        // POST: Lifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lift = await _context.Lift.FindAsync(id);
            if (lift != null)
            {
                _context.Lift.Remove(lift);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LiftExists(int id)
        {
            return _context.Lift.Any(e => e.Id == id);
        }
    }
}
