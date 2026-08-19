//<<<<<<< Updated upstream
using LibraryManagementSystem.Models;

//=======
//using Microsoft.EntityFrameworkCore;
//using LibraryManagementSystem.Models;

//namespace LibraryManagementSystem.Data
//{
//    public class LibraryDbContext : DbContext

//    {
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(
//     "Server=DESKTOP-PAS6CKN;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True");
//        }
//        public DbSet<User> Users { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Book> Books { get; set; }
//        public DbSet<Author> Authors { get; set; }
//        public DbSet<Payment> Payments { get; set; }
//        public DbSet<Review> Reviews { get; set; }
//        public DbSet<Read> Reads { get; set; }
//    }
//}
//using LibraryManagementSystem.Models;
//using Microsoft.EntityFrameworkCore;

//namespace LibraryManagementSystem.Data
//{
//    public class LibraryDbContext : DbContext
//    {
//        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<User> Users { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Book> Books { get; set; }
//        public DbSet<Author> Authors { get; set; }
//        public DbSet<Payment> Payments { get; set; }
//        public DbSet<Review> Reviews { get; set; }
//        public DbSet<Read> Reads { get; set; }
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//>>>>>>> Stashed changes
//using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    //=======
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public LibraryDbContext()
        {
        }

        //>>>>>>> Stashed changes

        //  {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
     "Server=.;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Read> Reads { get; set; }
    }
}