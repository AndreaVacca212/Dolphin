using Utility;

namespace Dolphin.Models
{
    public class Commento : Entity
    {
        public string ContenutoCommento { get; set; }
        public DateTime Data_Ora { get; set; }
        public int MiPiace { get; set; }
        public int idUtente { get; set; }
        public int idPost { get; set; }
        public Commento() 
        {

        }

    }
}
