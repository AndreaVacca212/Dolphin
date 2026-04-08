using System.ComponentModel.DataAnnotations;

namespace Dolphin.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required, MaxLength(800)]
        public string PostContent { get; set; } = string.Empty;

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int UserId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<LikePost> PostsLikes { get; set; } = new List<LikePost>();
    }
}
