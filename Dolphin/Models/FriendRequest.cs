using System;

namespace Dolphin.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }

        // Foreign keys
        public int IdApplicant { get; set; }
        public int IdReceiver { get; set; }

        // Navigation properties
        public User Applicant { get; set; } = null!;
        public User Receiver { get; set; } = null!;
    }
}
