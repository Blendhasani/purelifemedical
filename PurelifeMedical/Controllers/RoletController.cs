using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurelifeMedical.Data;
using PurelifeMedical.Models;
using PurelifeMedical.Services;
using PurelifeMedical.ViewModels.Rolet;

namespace PurelifeMedical.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RoletController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public RoletController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        // GET: Rolet
        public async Task<IActionResult> Index()
        {
            return _context.Rolet != null ?
                        View(await _context.Rolet.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Rolet'  is null.");
        }

        // GET: Rolet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rolet == null)
            {
                return NotFound();
            }

            var rolet = await _context.Rolet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolet == null)
            {
                return NotFound();
            }

            return View(rolet);
        }

        // GET: Rolet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rolet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddRoliViewModel rolet)
        {

            if (!ModelState.IsValid)
            {
                return View(rolet);
            }
            var user = _currentUser.GetCurrentUserName();
            var roli = new Rolet()
            {
                Emri = rolet.Emri,
                InsertedFrom = user,
                InsertedDate = DateTime.Now
            };
            _context.Add(roli);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Rolet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rolet == null)
            {
                return NotFound();
            }

            var rolet = await _context.Rolet.FindAsync(id);
            if (rolet == null)
            {
                return NotFound();
            }
            return View(rolet);
        }

        // POST: Rolet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,ModifiedDate,ModifiedFrom")] EditRoliViewModel rolet)
        {
            if (id != rolet.Id)
            {
                return NotFound();
            }
            var user = _currentUser.GetCurrentUserName();
            var roli = await _context.Rolet.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    roli.Emri = rolet.Emri;
                    roli.ModifiedDate = DateTime.Now;
                    roli.ModifiedFrom = user;
                    _context.Update(roli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoletExists(rolet.Id))
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
            return View(rolet);
        }

        // GET: Rolet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rolet == null)
            {
                return NotFound();
            }

            var rolet = await _context.Rolet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolet == null)
            {
                return NotFound();
            }

            return View(rolet);
        }

        // POST: Rolet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rolet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rolet'  is null.");
            }
            var rolet = await _context.Rolet.FindAsync(id);
            if (rolet != null)
            {
                _context.Rolet.Remove(rolet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoletExists(int id)
        {
            return (_context.Rolet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
