using Utility;

namespace Dolphin.Models
{
    public class Amicizia: Entity
    {
        public int IdUtente { get; set; }
        public int IdUtente2 { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string FotoProfilo { get; set; }
        public int UtenteId { get; set; }
        public Amicizia() { }
    }
}

