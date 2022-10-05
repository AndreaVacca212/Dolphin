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
  
                    return RedirectToAction("UserAccount", "User");
            }
            else
                return Redirect("Index");
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


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

