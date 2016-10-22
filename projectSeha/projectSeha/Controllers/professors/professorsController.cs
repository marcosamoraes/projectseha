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
        public ActionResult Professors()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}