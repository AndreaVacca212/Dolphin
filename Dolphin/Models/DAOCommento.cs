using System;
using Utility;

namespace Dolphin.Models
{
    public class DAOCommento
    {
        private Database db;
        public static DAOCommento instance = null;

        private DAOCommento()
        {
            db = new Database("Dolphin");
        }
        public static DAOCommento GetInstance()
        {
            return instance == null ? new DAOCommento() : instance;

        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Commenti ");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Commento a = new Commento();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }

        public bool Delete(int id)
        {
            return db.Send($"DELETE FROM Commenti WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            Commento commento = (Commento)e;

            return db.Send($"INSERT INTO Commenti " +
                             $"(idPost, idUtente, contenutoCommento, data_ora  ) " +
                             $"VALUES " +
                             $"({commento.IdPost}, {commento.IdUtente}, " +
                             $"'{commento.ContenutoCommento}', GETDATE()); ");   
        }

        public bool Send(Entity e)
        {
            Commento commento = (Commento)e;
            return db.Send(
                             $"UPDATE Commenti SET " +
                             $"contenutoPost = {commento.IdPost}," +
                             $"data_ora = {commento.IdUtente}," +
                             $"miPiace = {commento.ContenutoCommento}, " +
                             $"idUtente = '{commento.Data_Ora}' " +
                             $"idUtente = {commento.MiPiace} " +
                             $"WHERE id = {commento.Id}"
                            );
        }

        public List<Commento> ListaCommenti(int id)
        {
            List<Commento> ris = new List<Commento>();


            List<Dictionary<string, string>> tabella = db.Read($"select Utenti.nome as nomeCommento, Utenti.cognome as cognomeCommento, Commenti.contenutoCommento, Commenti.id as idcommento, posts.id as idPost " +
                                                               $"from Commenti inner join Posts on Commenti.idPost = Posts.id inner join Utenti on Utenti.id = Commenti.idUtente " +
                                                               $" where Commenti.idPost = {id} ");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Commento a = new Commento();

                a.FromDictionary(riga);


                ris.Add(a);

            }

            return ris;
        }

        public List<Entity> ReadCommenti(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT Utenti.nome, utenti.cognome,Utenti.fotoProfilo, Commenti.contenutoCommento, Commenti.data_ora FROM Commenti INNER JOIN Utenti on Utenti.id = Commenti.idUtente WHERE Commenti.idPost = {id}; ");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Commento a = new Commento();

                a.FromDictionary(riga);


                ris.Add(a);

            }
            return ris;

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

