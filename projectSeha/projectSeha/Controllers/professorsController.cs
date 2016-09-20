using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class professorsController : Controller
    {
        // GET: professors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Professors()
        {
            return View();
        }
    }
}