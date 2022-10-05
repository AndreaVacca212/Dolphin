﻿using Utility;
namespace Dolphin.Models
{
    public class Utente : Entity
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string email { get; set; }
        public string Pass { get; set; }
        public string Indirizzo { get; set; }
        public string Codice_Fiscale { get; set; }
        public string FotoProfilo { get; set; }
        public int IdPost { get; set; }
        public int IdCommento { get; set; }
        public int IdAmicizia { get; set; }

        public Utente()
        {

        }
    }
}