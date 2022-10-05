

namespace Dolphin.Models
{
    public class DAOCommento : IDAO
    {
        private Database db;
        public static DAOCommento instance = null;

        private DAOCommento()
        {
            db = new Database("Dolphin");
        }
        public static DAOCommento GetInstance()
        {
            if (instance == null)
                return instance = new DAOCommento();

            else
                return instance;

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
            return db.Update($"DELETE FROM Commenti WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            return db.Update($"INSERT INTO Commenti " +
                             $"(idPost, idUtente, contenutoCommento, data_ora, miPiace  ) " +
                             $"VALUES " +
                             $"({((Commento)e).IdPost}, {((Commento)e).IdUtente}, " +
                             $"'{((Commento)e).ContenutoCommento}', '{((Commento)e).Data_ora}', " +
                             $"{((Commento)e).MiPiace})");   
        }

        public bool Send(Entity e)
        {
            return db.Update(
                             $"UPDATE Commenti SET " +
                             $"contenutoPost = {((Commento)e).IdPost}," +
                             $"data_ora = {((Commento)e).IdUtente}," +
                             $"miPiace = {((Commento)e).ContenutoCommento}, " +
                             $"idUtente = '{((Commento)e).Data_ora}' " +
                             $"idUtente = {((Commento)e).MiPiace} " +
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
