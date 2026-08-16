using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{

    public class BookController : Controller
    {
        private readonly LibraryDbContext _dbContext;

        public BookController(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var Books = _dbContext.Books.ToList();
            return View("Index", Books);
        }
        public IActionResult BookDetails(int id)
        {
            var Books = _dbContext.Books.Include(x => x.Author).Include(x => x.Category).FirstOrDefault(x => x.BookId == id);
            if (Books == null)
            {
                return NotFound();
            }

            return View("BookDetails", Books);
        }
        public IActionResult Search(string search)
        {
            var books = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Title.Contains(search) || b.Author.Name.Contains(search));

            }

            return View("Search", books.ToList());
        }
        public IActionResult FreeBooks()
        {
            var freebooks = _dbContext.Books.Include(X => X.Author)
                                           .Include(X => X.Category)
                                           .Where(X => X.Price == 0).ToList();
            return View("FreeBooks", freebooks);
        }
        public IActionResult Add()
        {
            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();

            return View("Add");
        }
        [HttpPost]
        public IActionResult SaveAdd(Book book)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Authors = _dbContext.Authors.ToList();
                ViewBag.Categories = _dbContext.Categories.ToList();

                return View("Add", book);
            }

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var book = _dbContext.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Authors = _dbContext.Authors.ToList();
            ViewBag.Categories = _dbContext.Categories.ToList();

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Authors = _dbContext.Authors.ToList();
                ViewBag.Categories = _dbContext.Categories.ToList();

                return View(book);
            }

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult SaveDelete(int id)
        {
            var book = _dbContext.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult FilterByCategory(int id)
        {
            var books = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.CategoryId == id)
                .ToList();

            ViewBag.CategoryName = _dbContext.Categories
                .Where(c => c.CategoryId == id)
                .Select(c => c.Name)
                .FirstOrDefault();

            return View(books);
        }
        public IActionResult PaidBooks()
        {
            var paidBooks = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Where(b => b.Price > 0)
                .ToList();

            return View(paidBooks);
        }
        public IActionResult Read(int id)
        {
            var book = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }


    }
}
