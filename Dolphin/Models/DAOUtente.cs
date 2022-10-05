

namespace Dolphin.Models
{
    public class DAOUtente : IDAO
    {
        private Database db;
        public static DAOUtente instance = null;

        private DAOUtente()
        {
            db = new Database("Dolphin");
        }
        public static DAOUtente GetInstance()
        {
            if (instance == null)
               return instance = new DAOUtente();
            
            else
                return instance;
            
        }

        public List<Entity> Read()
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Utenti ");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Utente a = new Utente();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }

        public bool Delete(int id)
        {
            return db.Update($"DELETE FROM Utenti WHERE id = {id}");
        }

        public bool Insert(Entity e)
        {
            return db.Update($"INSERT INTO Utenti " +
                             $"(nome, cognome, telefono, email, pass, indirizzo, codice_fiscale, fotoProfilo) " +
                             $"VALUES " +
                             $"('{((Utente)e).Nome}', '{((Utente)e).Cognome}', " +
                             $"'{((Utente)e).Telefono}', '{((Utente)e).Email}', '{((Utente)e).Pass}'," +
                             $"'{((Utente)e).Indirizzo}', '{((Utente)e).Codice_fiscale}', '{((Utente)e).FotoProfilo}')");                                                         )");
        }

        public bool Send(Entity e)
        {
            return db.Update(
                             $"UPDATE Utenti SET " +
                             $"nome = '{((Utente)e).Nome}'," +
                             $"cognome = '{((Utente)e).Cognome}'," +
                             $"telefono = {((Utente)e).Telefono}, " +
                             $"email = '{((Utente)e).Email}' " +
                             $"pass = '{((Utente)e).Pass}'," +
                             $"indirizzo = '{((Utente)e).Indirizzo}'," +
                             $"codice_Fiscale = '{((Utente)e).Codice_Fiscale}'," +
                             $"fotoProfilo = '{((Utente)e).FotoProfilo}'," +
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

        public bool Valida(string username, string psw)
        {
            string query = $"SELECT * FROM Utenti WHERE email = '{email}' AND pass = '{pass}';";

            Dictionary<string, string> riga = db.ReadOne(query);

            if (riga != null)
                return true;
            else
                return false;
        }
   





    }
}
