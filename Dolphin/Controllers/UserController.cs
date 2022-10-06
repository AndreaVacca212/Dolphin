using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolphin.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dolphin.Controllers
{
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult UserAccount()
        {
            return View(DAOUtente.GetInstance().Read());
        }

        public IActionResult ModificaProfilo(int id)
        {
            return View(DAOUtente.GetInstance().Cerca(id));
        }

        public IActionResult ModificaUtente(Dictionary<string, string> parametri)
        {
            Utente user = new Utente();
            user.FromDictionary(parametri);

            if (DAOUtente.GetInstance().Modifica(user))
                return RedirectToAction("UserAccount","User");
            else
                return Content("Modifica Fallita");
        }


    }
}

