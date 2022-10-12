using System;
using Utility;

namespace Dolphin.Models
{
    public class RichiestaAmicizia : Entity
    {

            public int IdRichiedente { get; set; }
            public int IdRicevente { get; set; }
            public string Nome { get; set; }
            public string Cognome { get; set; }
            public string FotoProfilo { get; set; }
            public RichiestaAmicizia() { }
    }
}
