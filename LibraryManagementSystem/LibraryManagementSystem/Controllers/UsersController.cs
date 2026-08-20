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
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data; 
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


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, User user)
        //{
        //    if (id != user.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(user);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(GetAll));
        //    }

        //    return View(user);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

           
            ModelState.Remove("Password");
            ModelState.Remove("Books");
            ModelState.Remove("Reads");
            ModelState.Remove("Reviews");

            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

              
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;

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
                _context.SaveChanges(); 
                return RedirectToAction("Login");
            }
            return View(user);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Please enter both email and password.";
                return View();
            }

            // تنظيف المدخلات من المسافات
            var cleanEmail = email.Trim().ToLower();
            var cleanPassword = password.Trim();

            // البحث مع تجاهل حالة الأحرف في الإيميل ومطابقة الباسورد
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email.ToLower().Trim() == cleanEmail && u.Password.Trim() == cleanPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            // حفظ الجلسة
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role ?? "User");

            return RedirectToAction("Dashboard");
        }
        public IActionResult Register()
        {
            return View();
        }

        // POST: بيستقبل بيانات المستخدم الجديد ويحفظها
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            // نتأكد إن الإيميل مش مستخدم قبل كده
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ViewBag.Error = "This email is already registered";
                return View();
            }

            user.Role = "User"; // كل حساب جديد بيبقى User عادي افتراضيًا
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // يعرض صفحة الداشبورد بعد تسجيل الدخول
        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        // GET: يعرض صفحة كتابة الإيميل والباسورد الجديد
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: يتحقق من الإيميل ويحدّث الباسورد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                ViewBag.Error = "Please fill in all fields.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            // البحث عن المستخدم بالإيميل
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Error = "No account found with this email address.";
                return View();
            }

            // تحديث كلمة المرور
            user.Password = newPassword;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Password has been reset successfully! Please login.";
            return RedirectToAction("Login");
        }
    }
}