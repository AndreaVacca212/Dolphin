using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Dolphin.Models;
using Microsoft.AspNetCore.Mvc;
using Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dolphin.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> il;

        public static Utente utenteLoggato = null;

        private static int chiamata = -1;

        public HomeController(ILogger<HomeController> l)
        {
            il = l;
        }


        public IActionResult Valida(Dictionary<string, string> parametri)
        {
            if (DAOUtente.GetInstance().Valida(parametri["email"], parametri["pass"]))
            {
                // se arrivo qui significa che l'utente esiste
                il.LogInformation($"UTENTE LOGGATO: {parametri["email"]}");

                utenteLoggato = DAOUtente.GetInstance().Cerca(parametri["email"]);

                return RedirectToAction("Bacheca", "Home");
            }
            else
                return Redirect("Index");
        }

        public IActionResult Bacheca()
        {
            return View(DAOBacheca.GetInstance().Read(utenteLoggato.Id));
        }


        public IActionResult EliminaLike(int id)
        {
            if (DAOBacheca.GetInstance().DeleteLikes(id))
            {
                return RedirectToAction("Bacheca", "Home");
            }
            else
                return Content("Elimina like fallito");
        }
        public IActionResult Successo()
        {
            return View();
        }
        public IActionResult EliminaPost(int id)
        {
            if (DAOBacheca.GetInstance().DeletePost(id))
            {
                return RedirectToAction("Bacheca", "Home");
            }
            else
                return Content("Elimina like fallito");
        }


        public IActionResult NuovoLike(int id)
        {
            if (DAOBacheca.GetInstance().InsertLikes(id))
            {
                return RedirectToAction("Bacheca", "Home");
            }
            else
                return Content("Like fallito");
        }
        public IActionResult Salva(Dictionary<string, string> parametri)
        {
            Utente u = new Utente();

            u.FromDictionary(parametri);

            if (DAOUtente.GetInstance().Insert(u))
                return RedirectToAction("Successo", "Home");
            else
                return Content("Registrazione fallita");
        }

        public IActionResult Logout()
        {
            chiamata = -1;
            il.LogInformation($"LOGOUT: {utenteLoggato.Nome}");
            utenteLoggato = null;

            return RedirectToAction("Index","Home");
        }

        public IActionResult UserList()
        {
            return View(DAOUtente.GetInstance().Read3(HomeController.utenteLoggato.Id));
        }



        public IActionResult AccettaRichiestaAmicizia(int idRichiesta, int idRichiedente, int idAccettante)
        {

            if (DAOAmicizia.GetInstance().Insert(idRichiedente, idAccettante) && DAOAmicizia.GetInstance().Insert(idAccettante, idRichiedente))
            {
                if (DAORichiestaAmicizia.GetInstance().Delete(idRichiesta))
                {
                    return RedirectToAction("Bacheca", "Home");

                } else
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
                return RedirectToAction("Bacheca", "Home");
            }
            else
            {
                return Content("Ops non sono riuscito a cancellare");
            }
        }


        public IActionResult InserisciPost(Dictionary<string, string> parametri)
        {
            Post post = new Post();
            post.FromDictionary(parametri);

            if (DAOPost.GetInstance().Insert(post))
                return RedirectToAction("Bacheca", "Home");
            else
                return Content("Inserimento fallito");
        }

        public IActionResult UserListLike(string valore)
        {
            return View(DAOUtente.GetInstance().ReadLike(valore));
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}


