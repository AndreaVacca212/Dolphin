

namespace Dolphin.Models
{
    public class DAOAmicizia : IDAO
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

        public bool Delete(int id)
        {
            return db.Update($"DELETE FROM Amicizie WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            return db.Update($"INSERT INTO Amicizie " +
                             $"(idUtente, idUtente2) " +
                             $"VALUES " +
                             $"({((Amicizia)e).IdUtente}, {((Amicizia)e).IdUtente2})");
           

        }

        public bool Send(Entity e)
        {
            return db.Update(
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
