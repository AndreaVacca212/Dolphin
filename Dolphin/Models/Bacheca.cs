using System;
using Utility;

namespace Dolphin.Models
{
    public class Bacheca : Entity
    {
        public string NomeUtente { get; set; }
        public string CognomeUtente { get; set; }
        public string FotoUtente { get; set; }
        public string ContenutoPost { get; set; }
        public DateTime Data_Ora { get; set; }
        public string ContenutoCommento { get; set; }
        public int IdPost { get; set; }
        public int IdCommento { get; set; }
       // public int MiPiace { get; set; }

        public Bacheca() { }
    }
}
