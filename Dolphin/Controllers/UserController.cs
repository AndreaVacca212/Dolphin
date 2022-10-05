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
    }
}

