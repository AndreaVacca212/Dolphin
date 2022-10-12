using System;
using Utility;

namespace Dolphin.Models
{
    public class DAOAmicizia
    {
        private Database db;
        public static DAOAmicizia instance = null;

        private DAOAmicizia()
        {
            db = new Database("Dolphin");
        }
        public static DAOAmicizia GetInstance()
        {
            if (instance == null)
                return instance = new DAOAmicizia();

            else
                return instance;

        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Amicizie ");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Amicizia a = new Amicizia();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }

        public List<int> LeggiAmicizia(int id)
        {
            List<int> ris = new List<int>();

            List<Dictionary<string, string>> tabella = db.Read($"select idUtente2 from Amicizie where idUtente={id}");

            foreach (Dictionary<string, string> riga in tabella)
            {
                int a = 0;

                ris.Add(a);
            }

            return ris;
        }

        public List<Entity> ReadAmicizia(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT Utenti2.id, Utenti2.nome, Utenti2.cognome, Utenti2.fotoProfilo FROM Utenti INNER JOIN Amicizie ON Utenti.id = Amicizie.idUtente INNER JOIN Utenti AS Utenti2 ON Amicizie.idUtente2 = Utenti2.id WHERE utenti.id={id} OR Amicizie.idUtente = {id};");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Amicizia a = new Amicizia();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }

        public bool Delete(int id)
        {
            return db.Send($"DELETE FROM Amicizie WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            return db.Send($"INSERT INTO Amicizie " +
                             $"(idUtente, idUtente2) " +
                             $"VALUES " +
                             $"({((Amicizia)e).IdUtente}, {((Amicizia)e).IdUtente2})");
           
        }

        public bool Insert(int idRichiedente, int idAccettante)
        {
            return db.Send($"INSERT INTO Amicizie " +
                             $"(idUtente, idUtente2) " +
                             $"VALUES " +
                             $"({idRichiedente}, {idAccettante});");

        }

        public bool Send(Entity e)
        {
            return db.Send(
                             $"UPDATE Amicizie SET " +
                             $"idUtente = {((Amicizia)e).IdUtente}," +
                             $"idUtente2 = {((Amicizia)e).IdUtente2}," +
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

