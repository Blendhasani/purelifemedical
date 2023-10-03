using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurelifeMedical.Data;
using PurelifeMedical.Models;
using PurelifeMedical.Services;
using PurelifeMedical.ViewModels.Nacionaliteti;

namespace PurelifeMedical.Controllers
{

	[Authorize(Roles = "ADMIN")]
	public class NacionalitetiController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ICurrentUser _currentUser;
		public NacionalitetiController(ApplicationDbContext context, ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		// GET: Nacionalitetis
		public async Task<IActionResult> Index()
		{
			return _context.Nacionaliteti != null ?
						View(await _context.Nacionaliteti.ToListAsync()) :
						Problem("Entity set 'ApplicationDbContext.Nacionaliteti'  is null.");
		}

		// GET: Nacionalitetis/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Nacionaliteti == null)
			{
				return NotFound();
			}

			var nacionaliteti = await _context.Nacionaliteti
				.FirstOrDefaultAsync(m => m.Id == id);
			if (nacionaliteti == null)
			{
				return NotFound();
			}

			return View(nacionaliteti);
		}

		// GET: Nacionalitetis/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Nacionalitetis/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AddNacionalitetiViewModel nacionalitetiVM)
		{

			if (!ModelState.IsValid)
			{
				return View(nacionalitetiVM);
			}
			var user = _currentUser.GetCurrentUserName();

			var nacionalitetiNew = new Nacionaliteti()
			{
				Emri = nacionalitetiVM.Emri,
				InsertedFrom = user,
				InsertedDate = DateTime.Now

			};

			_context.Add(nacionalitetiNew);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

		}

		// GET: Nacionalitetis/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Nacionaliteti == null)
			{
				return NotFound();
			}

			var nacionaliteti = await _context.Nacionaliteti.FindAsync(id);
			if (nacionaliteti == null)
			{
				return NotFound();
			}
			return View(nacionaliteti);
		}

		// POST: Nacionalitetis/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Emri")] EditNacionalitetiViewModel nacionalitetiVM)
		{
			if (id != nacionalitetiVM.Id)
			{
				return NotFound();
			}

			var nacionalitetii = _context.Nacionaliteti.Where(x => x.Id == id).FirstOrDefault();
			var user = _currentUser.GetCurrentUserName();

			if (ModelState.IsValid)
			{
				try
				{
					nacionalitetii.Id = id;
					nacionalitetii.Emri = nacionalitetiVM.Emri;
					nacionalitetii.ModifiedFrom = user;
					nacionalitetii.ModifiedDate = DateTime.Now;
					_context.Update(nacionalitetii);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!NacionalitetiExists(nacionalitetiVM.Id))
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
			return View(nacionalitetiVM);
		}

		// GET: Nacionalitetis/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Nacionaliteti == null)
			{
				return NotFound();
			}

			var nacionaliteti = await _context.Nacionaliteti
				.FirstOrDefaultAsync(m => m.Id == id);
			if (nacionaliteti == null)
			{
				return NotFound();
			}

			return View(nacionaliteti);
		}

		// POST: Nacionalitetis/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Nacionaliteti == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Nacionaliteti'  is null.");
			}
			var nacionaliteti = await _context.Nacionaliteti.FindAsync(id);
			var stafiNacionalitet = await _context.Stafi.Where(x => x.NacionalitetiId == nacionaliteti.Id).ToListAsync();
			if (nacionaliteti != null)
			{
				_context.Stafi.RemoveRange(stafiNacionalitet);
				await _context.SaveChangesAsync();
				_context.Nacionaliteti.Remove(nacionaliteti);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool NacionalitetiExists(int id)
		{
			return (_context.Nacionaliteti?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
