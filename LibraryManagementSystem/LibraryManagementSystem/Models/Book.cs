using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
            [Key]
            public int BookId { get; set; }

            [Required]
            [StringLength(200)]
            public string Title { get; set; } = string.Empty;

            public int PublicationYear { get; set; }

            [StringLength(1000)]
            public string? Description { get; set; }

            [StringLength(500)]
            public string? CoverImage { get; set; }

            [Required]
            public string PdfUrl { get; set; } = string.Empty;

            [Range(0, 1000000)]
            [Column(TypeName = "decimal(18,2)")]
            public decimal Price { get; set; }

            public int CategoryId { get; set; }

            public int AuthorId { get; set; }




            [ForeignKey(nameof(CategoryId))]
            public Category Category { get; set; } = null!;

            [ForeignKey(nameof(AuthorId))]
            public Author Author { get; set; } = null!;




            public ICollection<Review> Reviews { get; set; }= new List<Review>();

            public ICollection<Payment> Payments { get; set; } = new List<Payment>();

            public ICollection<Read> Reads { get; set; }= new List<Read>();
        }
}
