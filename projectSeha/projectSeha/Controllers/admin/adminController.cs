using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
        public ActionResult Semesters()
        {
            return View();
        }
        public ActionResult Steps()
        {
            return View();
        }
        public ActionResult Password()
        {
            return View();
        }
    }
}