using Utility;

namespace Dolphin.Models
{
    public class Commento : Entity
    {
        public string ContenutoCommento { get; set; }
        public DateTime Data_Ora { get; set; }
        public int MiPiace { get; set; }
        public int IdUtente { get; set; }
        public int IdPost { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string FotoProfilo { get; set; }
        public Commento() 
        {
        }

    }
}