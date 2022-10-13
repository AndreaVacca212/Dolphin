using Utility;
using System;

namespace Dolphin.Models
{
    public class DAOPost
    {
        private Database db;
        public static DAOPost instance = null;

        private DAOPost()
        {
            db = new Database("Dolphin");
        }
        public static DAOPost GetInstance()
        {
            if (instance == null)
                return instance = new DAOPost();

            else
                return instance;

        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Posts;");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Post a = new Post();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }

        public List<Entity> ReadUtente(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT Posts.id, Posts.contenutoPost, Posts.data_ora, Utenti.nome, Utenti.cognome, utenti.fotoProfilo FROM Posts INNER JOIN Utenti on Utenti.id = Posts.idUtente WHERE Posts.idUtente = {id} ORDER BY Posts.data_ora desc;");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Post a = new Post();
                a.FromDictionary(riga);

                ris.Add(a);
                a.MiPiacePosts = (DAOPost.GetInstance().ReadLikes(a.Id));
                a.ListaCommenti = (DAOCommento.GetInstance().ListaCommenti(a.Id));
            }

            return ris;
        }

       /* public List<Entity> Read2(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Posts WHERE id = {id}");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Post post = new Post();
                post.FromDictionary(riga);

                ris.Add(post);

                post.MiPiacePosts = (DAOPost.GetInstance().ReadLikes(post.Id));
            }

            return ris;

        }*/

        public List<Entity> ReadLikes(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"select * FROM MiPiacePosts WHERE idPost = {id};");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Post a = new Post();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }

        public bool Delete(int id)
        {
            return db.Send($"DELETE FROM Posts WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            Post post = (Post)e;
            return db.Send($"INSERT INTO Posts " +
                             $"(contenutoPost, data_ora, idUtente) " +
                             $"VALUES " +
                             $"('{post.ContenutoPost}', GETDATE(), " +
                             $"{post.IdUtente})");
                                                                 
        }

       

        public bool Send(Entity e)
        {
            Post post = (Post)e;
            return db.Send(
                             $"UPDATE Posts SET " +
                             $"contenutoPost = '{post.ContenutoPost}'," +
                             $"data_ora = '{post.Data_Ora}'," +
                             $"idUtente = {post.IdUtente} " +
                             $"WHERE id = {post.Id}"
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

