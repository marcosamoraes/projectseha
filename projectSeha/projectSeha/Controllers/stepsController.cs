using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class stepsController : Controller
    {
        // GET: steps
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Steps()
        {
            return View();
        }
    }
}