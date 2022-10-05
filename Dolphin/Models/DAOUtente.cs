using System;
using Utility;

namespace Dolphin.Models
{
    public class DAOUtente
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
            return db.Send($"DELETE FROM Utenti WHERE id = {id}");
        }

        public bool Insert(Utente u)
        {
            return db.Send($"INSERT INTO Utenti " +
                             $"(nome, cognome, telefono, email, pass, indirizzo, codice_fiscale, fotoProfilo) " +
                             $"VALUES " +
                             $"('{u.Nome}', '{u.Cognome}', " +
                             $"'{u.Telefono}', '{u.Email}', '{u.Pass}'," +
                             $"'{u.Indirizzo}', '{u.Codice_Fiscale}', '{u.FotoProfilo}')");                                         
        }

        public bool Modifica(Entity e)
        {
            return db.Send(
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

        public bool Valida(string email, string pass)
        {
            string query = $"SELECT * FROM Utenti WHERE email = '{email}' AND pass = HASHBYTES('SHA2_256','{pass}');";

            Dictionary<string, string> riga = db.ReadOne(query);

            if (riga != null)
                return true;
            else
                return false;
        }

        public Utente Cerca(int id)  // Cerca per id
        {
            string query = $"SELECT * FROM Utenti WHERE id = {id}";
            //Inutile fare una lista di dictionary quando cerchiamo l'id perché è univoco
            Dictionary<string, string> riga = db.ReadOne(query);

            Utente user = new Utente();
            user.FromDictionary(riga);

            return user;
        }

        public Utente Cerca(string email)  // cerca per email
        {
            string query = $"SELECT * FROM Utenti WHERE email = '{email}'";
            //Inutile fare una lista di dictionary quando cerchiamo l'id perché è univoco
            Dictionary<string, string> riga = db.ReadOne(query);

            Utente user = new Utente();
            user.FromDictionary(riga);

            return user;
        }


    }
}
