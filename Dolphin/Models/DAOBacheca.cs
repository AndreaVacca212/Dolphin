using System;
using Dolphin.Controllers;
using Utility;

namespace Dolphin.Models
{
    public class DAOBacheca
    {
        private Database db;
        public static DAOBacheca instance = null;

        private DAOBacheca()
        {
            db = new Database("Dolphin");
        }
        public static DAOBacheca GetInstance()
        {
            if (instance == null)
                return instance = new DAOBacheca();

            else
                return instance;

        }
        public bool InsertLikes(int id)
        {
            return db.Send($"INSERT INTO MiPiacePosts " +
                           $"(idPost,idUtente) " +
                           $"VALUES " +
                           $"({id},{HomeController.utenteLoggato.Id})");
        }


        public bool DeletePost(int id)
        {
            return db.Send($"delete from Commenti where idPost={id}; delete from MiPiacePosts where idPost={id}; delete from posts where id={id}; ");
        }

        public List<Entity> Read(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT distinct Amici.idUtente as idUtente, Amici.nomeUtente, amici.cognomeUtente, Posts.contenutopost, Posts.data_ora, amici.fotoutente, posts.id as idPost " +
                                                               $"FROM Posts " +
                                                               $"INNER JOIN Amici ON Posts.idUtente=Amici.idAmicoLato1 OR Posts.idUtente = Amici.idAmicoLato2 " +
                                                               $"WHERE (Amici.idAmicoLato1={id} OR Amici.idAmicoLato2={id}) AND Posts.idUtente != {id} AND (Amici.idUtente != {id}) ORDER BY Posts.data_ora desc;");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Bacheca bacheca = new Bacheca();
                bacheca.FromDictionary(riga);

                ris.Add(bacheca);
            }

            return ris;
        }


        public bool DeleteLikes(int id)
        {
            string query = $"DELETE FROM MiPiacePosts WHERE idPost = {id} AND idUtente = {HomeController.utenteLoggato.Id};";
            Console.WriteLine("Query: " + query);
            return db.Send(query);
        }


    }
}
