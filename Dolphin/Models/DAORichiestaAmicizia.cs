using System;
using Utility;

namespace Dolphin.Models
{
    public class DAORichiestaAmicizia
    {
        private Database db;
        public static DAORichiestaAmicizia instance = null;

        private DAORichiestaAmicizia()
        {
            db = new Database("Dolphin");
        }
        public static DAORichiestaAmicizia GetInstance()
        {
            if (instance == null)
                return instance = new DAORichiestaAmicizia();

            else
                return instance;

        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM RichiesteAmicizia ");

            foreach (Dictionary<string, string> riga in tabella)
            {
                RichiestaAmicizia r = new RichiestaAmicizia();
                r.FromDictionary(riga);

                ris.Add(r);
            }

            return ris;
        }

        public List<Entity> ReadRichiestaAmicizia(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT RichiesteAmicizia.id, RichiesteAmicizia.idRichiedente, UtentiRichiedenti.nome, UtentiRichiedenti.cognome, UtentiRichiedenti.fotoProfilo FROM Utenti AS UtentiRichiedenti INNER JOIN RichiesteAmicizia ON UtentiRichiedenti.id = RichiesteAmicizia.idRichiedente INNER JOIN Utenti AS UtentiRiceventi ON UtentiRiceventi.id = RichiesteAmicizia.idRicevente WHERE UtentiRiceventi.id= {id} OR RichiesteAmicizia.idRicevente = {id};");

            foreach (Dictionary<string, string> riga in tabella)
            {
                RichiestaAmicizia r = new RichiestaAmicizia();
                r.FromDictionary(riga);

                ris.Add(r);
            }

            return ris;
        }

        public bool Delete(int id)
        {
            string query = $"DELETE FROM RichiesteAmicizia WHERE id = {id}";
            Console.WriteLine("Query: " + query);
            return db.Send(query);
        }

        public bool Insert(Entity e)
        {
            return db.Send($"INSERT INTO RichiesteAmicizia " +
                             $"(idRichiedente,idRicevente) " +
                             $"VALUES " +
                             $"({((RichiestaAmicizia)e).IdRichiedente}, {((RichiestaAmicizia)e).IdRicevente})");

        }

        public bool Insert(int idRichiedente, int idAccettante)
        {
            return db.Send($"INSERT INTO RichiesteAmicizia " +
                             $"(idRichiedente,idRicevente) " +
                             $"VALUES " +
                             $"({idRichiedente}, {idAccettante})");

        }

        public bool Send(Entity e)
        {
            return db.Send(
                             $"UPDATE RichiesteAmicizia SET " +
                             $"idRichiedente = {((RichiestaAmicizia)e).IdRichiedente}," +
                             $"idRicevente = {((RichiestaAmicizia)e).IdRicevente}," +
                             $"WHERE id = {e.Id}"
                            );
        }

        public Entity Find(int id)
        {
            foreach (Entity e in Read())
                if (e.Id == id)
                    return e;

            return null;
        }

    }
}

