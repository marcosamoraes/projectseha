using ProjectSeha.Entity;
using ProjectSeha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    [AutorizaAdmin]
    public class adminController : Controller
    {
        // GET: admin
        public ActionResult Semesters()
        {
           using (LembreteModel model = new LembreteModel())
           {
                List<Lembrete> listaLemb = model.Read();
                
                ViewBag.ListLembrete = listaLemb;
                return View();
           }
        }

        public ActionResult Steps()
        {
            return View();
        }
    }
}