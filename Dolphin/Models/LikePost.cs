namespace Dolphin.Models
{
    public class LikePost
    {
        public int Id { get; set; }

        // Foreign keys
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public Post Post { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}