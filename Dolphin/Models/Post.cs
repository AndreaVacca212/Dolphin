using Utility;

namespace Dolphin.Models
{
    public class Post: Entity
    {
        public string ContenutoPost { get; set; }
        public DateTime Data_Ora { get; set; }
        public int IdUtente { get; set; }
        public List<Entity>? MiPiacePosts { get; set; }
        public List<Commento>? ListaCommenti { get; set; }
        public Post ()
        {
        }
    }
}

