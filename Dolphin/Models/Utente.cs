using Utility;
namespace Dolphin.Models
{
    public class Utente : Entity
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Indirizzo { get; set; }
        public string Codice_Fiscale { get; set; }
        public string FotoProfilo { get; set; }
        public string Copertina { get; set; }
        public List<Entity> ListaPost { get; set; }
        public List<Entity> ListaAmicizie { get; set; }


       // public int IdPost { get; set; }
       // public string ContenutoPost { get; set; }
        //public DateTime Data_Ora { get; set; }
        //public int MiPiace { get; set; }

        public int IdCommento { get; set; }
        public int IdAmicizia { get; set; }

        public Utente()
        {

        }
    }
}
 