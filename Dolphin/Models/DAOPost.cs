

namespace Dolphin.Models
{
    public class DAOPost : IDAO
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
            return db.Update($"DELETE FROM Posts WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            return db.Update($"INSERT INTO Posts " +
                             $"(contenutoPost, data_ora, miPiace, idUtente) " +
                             $"VALUES " +
                             $"('{((Utente)e).ContenutoPost}', '{((Utente)e).Data_ora}', " +
                             $"{((Utente)e).MiPiace}, {((Utente)e).IdUtente})");
                                                                 
        }

        public bool Send(Entity e)
        {
            return db.Update(
                             $"UPDATE Posts SET " +
                             $"contenutoPost = '{((Utente)e).ContenutoPost}'," +
                             $"data_ora = '{((Utente)e).Data_ora}'," +
                             $"miPiace = {((Utente)e).MiPiace}, " +
                             $"idUtente = {((Utente)e).IdUtente} " +
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
