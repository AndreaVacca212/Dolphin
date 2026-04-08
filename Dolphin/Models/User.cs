using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dolphin.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telephone { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Address { get; set; }

        [MaxLength(16)]
        public string? FiscalCode { get; set; }

        [MaxLength(1000)]
        public string? ProfilePicture { get; set; }

        [MaxLength(2000)]
        public string? Cover { get; set; }

        [MaxLength(20)]
        public string Role { get; set; } = "user";

        // Navigation properties
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<LikePost> PostsLikes { get; set; } = new List<LikePost>();

        // Amicizie: un utente può essere lato 1 o lato 2
        public ICollection<Friendship> FriendsAsLato1 { get; set; } = new List<Friendship>();
        public ICollection<Friendship> FriendsAsLato2 { get; set; } = new List<Friendship>();

        public ICollection<FriendRequest> RequestsSent { get; set; } = new List<FriendRequest>();
        public ICollection<FriendRequest> RequestsReceived { get; set; } = new List<FriendRequest>();
    }
}
