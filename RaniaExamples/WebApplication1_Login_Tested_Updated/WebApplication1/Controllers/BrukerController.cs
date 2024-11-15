
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class BrukerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrukerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all Area Changes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AreaChanges.ToListAsync());
        }

        // Create Area Change (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Create Area Change (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GeoJson,Description")] AreaChange areaChange)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaChange);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(areaChange);
        }

        // Edit Area Change (GET)
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaChange = await _context.AreaChanges.FindAsync(id);
            if (areaChange == null)
            {
                return NotFound();
            }
            return View(areaChange);
        }

        // Edit Area Change (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,GeoJson,Description")] AreaChange areaChange)
        {
            if (id != areaChange.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaChange);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaChangeExists(areaChange.Id))
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
            return View(areaChange);
        }

        // Delete Area Change (GET)
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaChange = await _context.AreaChanges
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaChange == null)
            {
                return NotFound();
            }

            return View(areaChange);
        }

        // Delete Area Change (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var areaChange = await _context.AreaChanges.FindAsync(id);
            _context.AreaChanges.Remove(areaChange);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaChangeExists(string id)
        {
            return _context.AreaChanges.Any(e => e.Id == id);
        }
    }
}
