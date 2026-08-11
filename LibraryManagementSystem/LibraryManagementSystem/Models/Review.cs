using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? CommentText { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;




        public int UserId { get; set; }

        public int BookId { get; set; }


        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;
    }
}
