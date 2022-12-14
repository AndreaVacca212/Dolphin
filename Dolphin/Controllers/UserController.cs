using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Dolphin.Models;
using Dolphin.Controllers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dolphin.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult UserAccount()
        {
            return View(DAOUtente.GetInstance().Read2(HomeController.utenteLoggato.Id)[0]);
        }

        public IActionResult ViewProfile(int id)
        {
            return View(DAOUtente.GetInstance().Read2(id)[0]);
        }

        public IActionResult ModificaProfilo(int id)
        {
            return View(DAOUtente.GetInstance().Cerca(id));
        }

        public IActionResult ModificaProfilo2(int id)
        {
            return View(DAOUtente.GetInstance().Cerca(id));
        }

        public IActionResult ModificaUtente(Dictionary<string, string> parametri)
        {
            Utente user = new Utente();
            user.FromDictionary(parametri);

            if (DAOUtente.GetInstance().Modifica(user))
                return RedirectToAction("UserAccount", "User");
            else
                return Content("Modifica Fallita");
        }
        public IActionResult EliminaPost(int id)
        {
            if (DAOPost.GetInstance().Delete(id))
                return RedirectToAction("UserAccount", "User");
            else
                return Content("Eliminazione Fallita");
        }

        public IActionResult InserisciPost(Dictionary<string, string> parametri)
        {
            Post post = new Post();
            post.FromDictionary(parametri);

            if (DAOPost.GetInstance().Insert(post))
                return RedirectToAction("UserAccount", "User");
            else
                return Content("Inserimento fallito");
        }

        public IActionResult EliminaLike(int id)
        {
            if (DAOBacheca.GetInstance().DeleteLikes(id))
            {
                return RedirectToAction("UserAccount", "User");
            }
            else
                return Content("Elimina like fallito");
        }

        public IActionResult NuovoLike(int id)
        {
            if (DAOBacheca.GetInstance().InsertLikes(id))
            {
                return RedirectToAction("UserAccount", "User");
            }
            else
                return Content("Like fallito");
        }




        public IActionResult InserisciCommento(Dictionary<string, string> parametri)
        {
            Commento c = new Commento();
            c.FromDictionary(parametri);

            if (DAOCommento.GetInstance().Insert(c))
                return RedirectToAction("Bacheca", "Home");
            else
                return Content("Inserimento fallito");
        }

        public IActionResult RichiediAmicizia(int idRichiedente, int idAccettante)
        {
            if (DAORichiestaAmicizia.GetInstance().Insert(idRichiedente, idAccettante))
                return RedirectToAction("Bacheca", "Home");
            else
                return Content("Richiesta fallita");
        }

        public IActionResult AccettaRichiestaAmicizia(int idRichiesta, int idRichiedente, int idAccettante)
        {

            if (DAOAmicizia.GetInstance().Insert(idRichiedente, idAccettante) && DAOAmicizia.GetInstance().Insert(idAccettante, idRichiedente))
            {
                if (DAORichiestaAmicizia.GetInstance().Delete(idRichiesta))
                {
                    return RedirectToAction("UserAccount", "User");

                }
                else
                {
                    return Content("Ops qualcosa è andato storto 1");
                }
            }
            else
            {
                return Content("Ops qualcosa è andato storto");
            }
        }

        public IActionResult RifiutaRichiestaAmicizia(int idRichiesta)
        {
            if (DAORichiestaAmicizia.GetInstance().Delete(idRichiesta))
            {
                return RedirectToAction("UserAccount", "User");
            }
            else
            {
                return Content("Ops qualcosa è andato storto");
            }
        }

        public IActionResult EliminaUtente(int id)
        {
            if (DAOUtente.GetInstance().Delete(id))
            {
                return RedirectToAction("UserList", "Home");
            }
            else
            {
                return Content("Ops non sono riuscito a eliminare");
            }
        }
    }
}

