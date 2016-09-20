using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class userController : Controller
    {
        // GET: user
        public ActionResult Availability()
        {
            return View();
        }

        public ActionResult Password()
        {
            return View();
        }
    }
}