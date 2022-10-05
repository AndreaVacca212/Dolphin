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

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Posts ");

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
                             $"(contenutoPost, data_ora, miPiace, idUtente) " +
                             $"VALUES " +
                             $"('{post.ContenutoPost}', '{post.Data_Ora}', " +
                             $"{post.MiPiace}, {post.IdUtente})");
                                                                 
        }

        public bool Send(Entity e)
        {
            Post post = (Post)e;
            return db.Send(
                             $"UPDATE Posts SET " +
                             $"contenutoPost = '{post.ContenutoPost}'," +
                             $"data_ora = '{post.Data_Ora}'," +
                             $"miPiace = {post.MiPiace}, " +
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
