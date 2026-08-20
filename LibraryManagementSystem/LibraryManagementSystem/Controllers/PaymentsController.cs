using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly LibraryDbContext _context;

        public PaymentsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: /Payments/Create?bookId=1
        [HttpGet]
        public async Task<IActionResult> Create(int bookId)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookId == bookId);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            ViewBag.Book = book;

            return View();
        }

        // POST: /Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int bookId,
            int userId,
            string cardNumber,
            string cardHolder,
            string expiryDate,
            string cvv)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookId == bookId);

            if (book == null)
            {
                return NotFound("Book not found.");
            }

            // Test Visa validation
            if (string.IsNullOrWhiteSpace(cardNumber) ||
                cardNumber.Replace(" ", "").Length != 16)
            {
                ViewBag.Error = "Please enter a valid 16-digit test card number.";
                ViewBag.Book = book;
                return View();
            }

            if (string.IsNullOrWhiteSpace(cardHolder))
            {
                ViewBag.Error = "Please enter the card holder name.";
                ViewBag.Book = book;
                return View();
            }

            if (string.IsNullOrWhiteSpace(expiryDate))
            {
                ViewBag.Error = "Please enter the expiry date.";
                ViewBag.Book = book;
                return View();
            }

            if (string.IsNullOrWhiteSpace(cvv) || cvv.Length != 3)
            {
                ViewBag.Error = "Please enter a valid 3-digit CVV.";
                ViewBag.Book = book;
                return View();
            }

            var payment = new Payment
            {
                Amount = book.Price,
                PaymentDate = DateTime.Now,
                Status = "Paid",
                UserId = userId,
                BookId = book.BookId
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", new
            {
                paymentId = payment.PaymentId
            });
        }

        // GET: /Payments/Success?paymentId=1
        [HttpGet]
        public async Task<IActionResult> Success(int paymentId)
        {
            var payment = await _context.Payments
                .Include(p => p.Book)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null)
            {
                return NotFound("Payment not found.");
            }

            return View(payment);
        }
    }
}


//using LibraryManagementSystem.Data;
//using LibraryManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LibraryManagementSystem.Controllers
//{
//    public class PaymentsController : Controller
//    {
//        private readonly LibraryDbContext _context;

//        public PaymentsController(LibraryDbContext context)
//        {
//            _context = context;
//        }

//        // GET: /Payments/Create/5
//        [HttpGet]
//        [Route("Payments/Create/{bookId:int}")]
//        public async Task<IActionResult> Create(int bookId)
//        {
//            var book = await _context.Books
//                .FirstOrDefaultAsync(b => b.BookId == bookId);

//            if (book == null)
//            {
//                return NotFound("Book not found.");
//            }

//            var users = await _context.Users.ToListAsync();

//            ViewBag.Book = book;
//            ViewBag.Users = users;

//            return View();
//        }

//        // POST: /Payments/Create/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Route("Payments/Create/{bookId:int}")]
//        public async Task<IActionResult> Create(
//            int bookId,
//            int userId,
//            string cardNumber,
//            string cardHolder,
//            string expiryDate,
//            string cvv)
//        {
//            var book = await _context.Books
//                .FirstOrDefaultAsync(b => b.BookId == bookId);

//            if (book == null)
//            {
//                return NotFound("Book not found.");
//            }

//            // Check that UserId really exists
//            var user = await _context.Users
//                .FirstOrDefaultAsync(u => u.UserId == userId);

//            if (user == null)
//            {
//                ViewBag.Error = "The selected User does not exist.";
//                ViewBag.Book = book;
//                ViewBag.Users = await _context.Users.ToListAsync();

//                return View();
//            }

//            // Test card validation
//            if (string.IsNullOrWhiteSpace(cardNumber) ||
//                cardNumber.Replace(" ", "").Length != 16)
//            {
//                ViewBag.Error = "Please enter a 16-digit test card number.";
//                ViewBag.Book = book;
//                ViewBag.Users = await _context.Users.ToListAsync();

//                return View();
//            }

//            if (string.IsNullOrWhiteSpace(cardHolder))
//            {
//                ViewBag.Error = "Please enter the card holder name.";
//                ViewBag.Book = book;
//                ViewBag.Users = await _context.Users.ToListAsync();

//                return View();
//            }

//            if (string.IsNullOrWhiteSpace(expiryDate))
//            {
//                ViewBag.Error = "Please enter the expiry date.";
//                ViewBag.Book = book;
//                ViewBag.Users = await _context.Users.ToListAsync();

//                return View();
//            }

//            if (string.IsNullOrWhiteSpace(cvv) || cvv.Length != 3)
//            {
//                ViewBag.Error = "Please enter a valid 3-digit CVV.";
//                ViewBag.Book = book;
//                ViewBag.Users = await _context.Users.ToListAsync();

//                return View();
//            }

//            var payment = new Payment
//            {
//                Amount = book.Price,
//                PaymentDate = DateTime.Now,
//                Status = "Paid",
//                UserId = userId,
//                BookId = book.BookId
//            };

//            _context.Payments.Add(payment);

//            await _context.SaveChangesAsync();

//            return RedirectToAction("Success", new
//            {
//                paymentId = payment.PaymentId
//            });
//        }

//        // GET: /Payments/Success/1
//        [HttpGet]
//        [Route("Payments/Success/{paymentId:int}")]
//        public async Task<IActionResult> Success(int paymentId)
//        {
//            var payment = await _context.Payments
//                .Include(p => p.Book)
//                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

//            if (payment == null)
//            {
//                return NotFound("Payment not found.");
//            }

//            return View(payment);
//        }
//    }
//}

