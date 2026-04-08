namespace Dolphin.Models
{
    // Rappresenta un legame di amicizia già accettato.
    // Il vecchio schema aveva due righe speculari (A→B e B→A).
    // Con EF è più pulito avere una sola riga con i due lati,
    // e gestire la simmetria nelle query o nel service layer.
    public class Friendship
    {
        public int Id { get; set; }

        // Foreign keys
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }

        // Navigation properties
        public User User1 { get; set; } = null!;
        public User User2 { get; set; } = null!;
    }
}