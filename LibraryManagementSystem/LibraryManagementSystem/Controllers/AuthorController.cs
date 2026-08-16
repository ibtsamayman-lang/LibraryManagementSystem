using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorController : Controller
    {
        LibraryDbContext _context = new LibraryDbContext();
        public IActionResult AboutAuthor(int id)
        {
            var auth = _context.Authors.FirstOrDefault(a=>a.AuthorId == id);
            return View("AboutAuthor",auth);

        }
        public IActionResult AboutBook(int id) { 
            
            var book = _context.Books.FirstOrDefault(b=>b.BookId == id);
            if (book == null) {

                return NotFound();
            }
            
            var author = _context.Authors.FirstOrDefault(a=>a.AuthorId == book.AuthorId);
            var cate = _context.Categories.FirstOrDefault(c=>c.CategoryId == book.CategoryId);
            if (author == null) {
                return NotFound();
            }
            var details = new AuthorBook
            {
                TitleBook = book.Title,
                AuthorName = author.Name,
                CategoryDescription = cate.Description,
                publicationDate = book.PublicationYear

            };

            
            return View("AboutBook", details);
        }



    }
}
