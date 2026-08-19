//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LibraryManagementSystem.Models;
//using LibraryManagementSystem.Data;
//using System.Linq;

//namespace LibraryManagementSystem.Controllers
//{
//    public class UsersController : Controller
//    {
//        LibraryDbContext _context = new LibraryDbContext();
//        public IActionResult Index()
//        {
//            var user = _context.Users.ToList();
//            return View(user);
//        }
//        public IActionResult Add()
//        {
//            return View("Add");
//        }
//        public IActionResult Details(int id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var user =  _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }
//        public IActionResult Create()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Users.Add(user);
//                 _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }

//            return View(user);
//        }
//        public IActionResult Edit(int id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var user =_context.Users.Find(id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(int id, User user)
//        {
//            if (id != user.UserId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(user);
//                    _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserExists(user.UserId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }

//            return View(user);
//        }
//        private bool UserExists(int id)
//        {
//            return _context.Users.Any(u => u.UserId == id);
//        }
//        public IActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var user =  _context.Users
//                .FirstOrDefaultAsync(u => u.UserId == id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var user =  _context.Users.Find(id);

//            if (user == null)
//            {
//                return NotFound();
//            }

//            _context.Users.Remove(user);

//             _context.SaveChangesAsync();

//            return RedirectToAction("Index");
//        }
//    }
//}
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly LibraryDbContext _context;

        public UsersController(LibraryDbContext context)
        {
            _context = context;
        }
        

        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return View(users); 
        }


        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(u => u.UserId == id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAll));
            }

            return View(user);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAll));
            }

            return View(user);
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAll));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult User(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges(); // حفظ البيانات في SQL Server
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}