using Utility;

namespace Dolphin.Models
{
    public class Post: Entity
    {
        public string ContenutoPost { get; set; }
        public DateTime Data_Ora { get; set; }
        public int MiPiace { get; set; }
        public int IdUtente { get; set; }
        public Post ()
        {

        }
    }
}
