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

        public ActionResult CreateLemb(FormCollection form)
        {
            Lembrete l = new Lembrete();

            l.Conteudo = form["Lembrete"];

            using (LembreteModel model = new LembreteModel())
            {
                model.Create(l);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult DeleteLemb(int id)
        {
            using (LembreteModel model = new LembreteModel())
            {
                model.Delete(id);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Steps()
        {
            return View();
        }
    }
}