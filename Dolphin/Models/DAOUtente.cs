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

            List<Dictionary<string, string>> tabella = db.Read("SELECT * FROM Utenti;");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Utente a = new Utente();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }


        public List<Entity> ReadLike(string valore)
        {
            List<Entity> ris = new List<Entity>();
            List<Dictionary<string, string>> tabella = db.Read($"SELECT Utenti.id, Utenti.nome, Utenti.cognome, Utenti.fotoProfilo FROM Utenti WHERE Utenti.nome LIKE '%{valore}%' OR Utenti.cognome LIKE '%{valore}%';");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Utente user = new Utente();
                user.FromDictionary(riga);

                ris.Add(user);
            }
            return ris;
        }

        public List<Entity> LeggiUtente(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * FROM Utenti WHERE id = {id};");

            foreach (Dictionary<string, string> riga in tabella)
            {
                Utente a = new Utente();
                a.FromDictionary(riga);

                ris.Add(a);
            }

            return ris;
        }





        public List<Entity> Read2(int id)
        {
            List<Entity> ris = new List<Entity>();

            Dictionary<string, string> tabella = db.ReadOne($"SELECT * " +
                                                            $"FROM utenti " +
                                                            $"WHERE id = {id}");
                                                             
                Utente a = new Utente();
                a.FromDictionary(tabella);

                ris.Add(a);
           
                a.ListaPost = (DAOPost.GetInstance().ReadUtente(a.Id));
                
                a.ListaAmicizie = (DAOAmicizia.GetInstance().ReadAmicizia(a.Id));

            return ris;
        }




        public List<Entity> Read3(int id)
        {
            List<Entity> ris = new List<Entity>();

            List<Dictionary<string, string>> tabella = db.Read($"SELECT * " +
                                                               $"FROM Utenti " +
                                                               $"WHERE id != {id}  ;");
            foreach(Dictionary<string, string> riga in tabella)
            {
                Utente a = new Utente();
                a.FromDictionary(riga);

                ris.Add(a);

                a.ListaPost = (DAOPost.GetInstance().ReadUtente(a.Id));
                a.ListaAmicizie = (DAOAmicizia.GetInstance().ReadAmicizia(a.Id));
            }
            


            return ris;
        }

        public bool Delete(int id)
        {
            return db.Send($"delete from Commenti where idUtente={id} or idPost is null; delete from MiPiacePosts where idUtente={id};delete from RichiesteAmicizia where idRichiedente = {id} or idRicevente = {id}; delete from Amicizie where idUtente={id} or idUtente2={id}; delete from posts where idUtente={id}; delete from utenti where id={id};");
        }

        public bool Insert(Utente u)
        {
            return db.Send($"INSERT INTO Utenti " +
                             $"(nome, cognome, telefono, email, pass, indirizzo, codice_fiscale, fotoProfilo, copertina, ruolo) " +
                             $"VALUES " +
                             $"('{u.Nome}', '{u.Cognome}', " +
                             $"'{u.Telefono}', '{u.Email}',HASHBYTES('SHA2_256','{u.Pass}')," +
                             $"'{u.Indirizzo}', '{u.Codice_Fiscale}', '{u.FotoProfilo}', '{u.Copertina}','{u.Ruolo}')");                                         
        }

        public bool Modifica(Entity e)
        {
            Utente user = (Utente)e;
            return db.Send(
                             $"UPDATE Utenti SET " +
                             $"nome = '{user.Nome}'," +
                             $"cognome = '{user.Cognome}'," +
                             $"telefono = '{user.Telefono}', " +
                             $"email = '{user.Email}', " +
                             $"indirizzo = '{user.Indirizzo}', " +
                             $"codice_Fiscale = '{user.Codice_Fiscale}', " +
                             $"fotoProfilo = '{user.FotoProfilo}'," +
                             $"copertina = '{user.Copertina}' " +
                             $"WHERE id = {user.Id}"
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
