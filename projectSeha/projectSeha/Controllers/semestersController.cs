using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class semestersController : Controller
    {
        // GET: semesters
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Semesters()
        {
            return View();
        }
    }
}