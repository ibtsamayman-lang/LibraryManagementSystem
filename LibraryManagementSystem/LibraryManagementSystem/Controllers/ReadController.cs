using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class ReadController : Controller
    {
        private readonly LibraryDbContext _context;

        public ReadController(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reads = await _context.Reads
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();

            return View(reads);
        }

        public async Task<IActionResult> Details(int id)
        {
            var read = await _context.Reads
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.ReadId == id);

            if (read == null)
                return NotFound();

            return View(read);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Read read)
        {
            if (ModelState.IsValid)
            {
                read.ReadDate = DateTime.Now;

                _context.Reads.Add(read);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(read);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var read = await _context.Reads.FindAsync(id);

            if (read == null)
                return NotFound();

            return View(read);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Read read)
        {
            if (id != read.ReadId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Reads.Update(read);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(read);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var read = await _context.Reads
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.ReadId == id);

            if (read == null)
                return NotFound();

            return View(read);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var read = await _context.Reads.FindAsync(id);

            if (read != null)
            {
                _context.Reads.Remove(read);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}