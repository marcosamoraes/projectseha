using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ProjectSeha.Controllers
{
    public class assignmentController : Controller
    {
        // GET: assignment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Assignment()
        {
            return View();
        }

    }
}