using System.ComponentModel.DataAnnotations;

namespace Dolphin.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required, MaxLength(800)]
        public string ContentComment { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}