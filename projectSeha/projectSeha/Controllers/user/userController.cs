using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    [AutorizaProfessor]
    public class userController : Controller
    {
        // GET: user
        public ActionResult Availability()
        {
            return View();
        }
    }
}