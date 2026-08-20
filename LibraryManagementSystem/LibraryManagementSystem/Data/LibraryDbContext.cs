

//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//payments_ganna
//Stashed changes;
//using LibraryManagementSystem.Models;

using LibraryManagementSystem.Models;


namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
 
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public LibraryDbContext()
        {
        }


      
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