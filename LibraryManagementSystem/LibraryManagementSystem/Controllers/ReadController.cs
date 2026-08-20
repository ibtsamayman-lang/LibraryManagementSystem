//using LibraryManagementSystem.Data;
//using LibraryManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LibraryManagementSystem.Controllers
//{
//    public class ReadController : Controller
//    {
//        private readonly LibraryDbContext _context;

//        public ReadController(LibraryDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var reads = await _context.Reads
//                .Include(r => r.User)
//                .Include(r => r.Book)
//                .ToListAsync();

//            return View(reads);
//        }

//        public async Task<IActionResult> Details(int id)
//        {
//            var read = await _context.Reads
//                .Include(r => r.User)
//                .Include(r => r.Book)
//                .FirstOrDefaultAsync(r => r.ReadId == id);

//            if (read == null)
//                return NotFound();

//            return View(read);
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(Read read)
//        {
//            if (ModelState.IsValid)
//            {
//                read.ReadDate = DateTime.Now;

//                _context.Reads.Add(read);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Index));
//            }

//            return View(read);
//        }

//        public async Task<IActionResult> Edit(int id)
//        {
//            var read = await _context.Reads.FindAsync(id);

//            if (read == null)
//                return NotFound();

//            return View(read);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, Read read)
//        {
//            if (id != read.ReadId)
//                return NotFound();

//            if (ModelState.IsValid)
//            {
//                _context.Reads.Update(read);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(Index));
//            }

//            return View(read);
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var read = await _context.Reads
//                .Include(r => r.User)
//                .Include(r => r.Book)
//                .FirstOrDefaultAsync(r => r.ReadId == id);

//            if (read == null)
//                return NotFound();

//            return View(read);
//        }

//        [HttpPost, ActionName("Delete")]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var read = await _context.Reads.FindAsync(id);

//            if (read != null)
//            {
//                _context.Reads.Remove(read);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.Books = await _context.Books.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Read read)
        {
            ModelState.Remove("User");
            ModelState.Remove("Book");

            if (ModelState.IsValid)
            {
                read.ReadDate = DateTime.Now;

                _context.Reads.Add(read);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.Books = await _context.Books.ToListAsync();

            return View(read);
        }

      
public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var read = await _context.Reads.FindAsync(id);
        if (read == null)
        {
            return NotFound();
        }

        // جلب البيانات وتمرير اسم الـ Primary Key واسم العمود المعروض
        var usersList = await _context.Users.ToListAsync();
        var booksList = await _context.Books.ToListAsync();

        ViewBag.UsersList = new SelectList(usersList, "UserId", "Name", read.UserId);
        ViewBag.BooksList = new SelectList(booksList, "BookId", "Title", read.BookId);

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
        [ValidateAntiForgeryToken]
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