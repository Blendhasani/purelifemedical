using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurelifeMedical.Data;
using PurelifeMedical.Models;
using PurelifeMedical.Services;
using PurelifeMedical.ViewModels.Shteti;

namespace PurelifeMedical.Controllers
{
	[Authorize(Roles = "ADMIN")]
	public class ShtetiController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ICurrentUser _currentUser;
		public ShtetiController(ApplicationDbContext context, ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		// GET: Shteti
		public async Task<IActionResult> Index()
		{
			return _context.Shteti != null ?
						View(await _context.Shteti.ToListAsync()) :
						Problem("Entity set 'ApplicationDbContext.Shteti'  is null.");
		}

		// GET: Shteti/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Shteti == null)
			{
				return NotFound();
			}

			var shteti = await _context.Shteti
				.FirstOrDefaultAsync(m => m.Id == id);
			if (shteti == null)
			{
				return NotFound();
			}

			return View(shteti);
		}

		// GET: Shteti/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Shteti/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AddShtetiViewModel shtetiVM)
		{
			if (!ModelState.IsValid)
			{
				//error exception
				return View(shtetiVM);
			}

			var user = _currentUser.GetCurrentUserName();

			var shtetNew = new Shteti()
			{
				Emri = shtetiVM.Emri,
				InsertedFrom = user,
				InsertedDate = DateTime.Now
			};


			_context.Add(shtetNew);
			await _context.SaveChangesAsync();

			//success

			return RedirectToAction(nameof(Index));
		}

		// GET: Shteti/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Shteti == null)
			{
				return NotFound();
			}

			var shteti = await _context.Shteti.FindAsync(id);
			if (shteti == null)
			{
				return NotFound();
			}
			return View(shteti);
		}

		// POST: Shteti/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Emri")] EditShtetiViewModel shteti)
		{
			if (id != shteti.Id)
			{
				return NotFound();
			}

			var shtetii = _context.Shteti.Where(x => x.Id == id).FirstOrDefault();
			var user = _currentUser.GetCurrentUserName();

			if (ModelState.IsValid)
			{
				try
				{
					shtetii.Id = id;
					shtetii.Emri = shteti.Emri;
					shtetii.ModifiedFrom = user;
					shtetii.ModifiedDate = DateTime.Now;
					_context.Update(shtetii);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ShtetiExists(shteti.Id))
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
			return View(shteti);
		}

		// GET: Shteti/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Shteti == null)
			{
				return NotFound();
			}

			var shteti = await _context.Shteti
				.FirstOrDefaultAsync(m => m.Id == id);
			if (shteti == null)
			{
				return NotFound();
			}

			return View(shteti);
		}

		// POST: Shteti/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Shteti == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Shteti'  is null.");
			}
			var shteti = await _context.Shteti.FindAsync(id);
			var stafiShtet = await _context.Stafi.Where(x => x.ShtetiId == shteti.Id).ToListAsync();
			if (shteti != null)
			{
				_context.Stafi.RemoveRange(stafiShtet);
				await _context.SaveChangesAsync();
				_context.Shteti.Remove(shteti);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ShtetiExists(int id)
		{
			return (_context.Shteti?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
