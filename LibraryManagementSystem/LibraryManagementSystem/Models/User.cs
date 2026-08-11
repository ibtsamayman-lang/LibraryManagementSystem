using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
            [Key]
            public int UserId { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; } = string.Empty;

            [Required]
            [EmailAddress]
            [StringLength(150)]
            public string Email { get; set; } = string.Empty;

            [Required]
            public string Password { get; set; } = string.Empty;


           

            public ICollection<Payment> Payments { get; set; }= new List<Payment>();

            public ICollection<Review> Reviews { get; set; }= new List<Review>();

            public ICollection<Read> Reads { get; set; } = new List<Read>();
        }
    }

