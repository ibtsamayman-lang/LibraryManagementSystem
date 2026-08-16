using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly LibraryDbContext _context;

        public ReviewController(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .ToListAsync();

            return View(reviews);
        }

        public async Task<IActionResult> Details(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
                return NotFound();

            return View(review);
        }

        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.Users = await _context.Users.ToListAsync();
        //    ViewBag.Books = await _context.Books.ToListAsync();

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        review.ReviewDate = DateTime.Now;

        //        _context.Reviews.Add(review);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewBag.Users = await _context.Users.ToListAsync();
        //    ViewBag.Books = await _context.Books.ToListAsync();

        //    return View(review);
        //}
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.Books = await _context.Books.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            // نشيل الـNavigation Properties من الـValidation
            ModelState.Remove("User");
            ModelState.Remove("Book");

            if (ModelState.IsValid)
            {
                review.ReviewDate = DateTime.Now;

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // لو فيه Validation Error، نرجع البيانات للـDropdown
            ViewBag.Users = await _context.Users.ToListAsync();
            ViewBag.Books = await _context.Books.ToListAsync();

            return View(review);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (id != review.ReviewId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(review);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}