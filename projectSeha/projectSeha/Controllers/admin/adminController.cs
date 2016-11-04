using ProjectSeha.Entity;
using ProjectSeha.Models;
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
           // using (LembreteModel model = new LembreteModel())
           // {
                //List<Lembrete> listaLemb = model.Read();
                List<Lembrete> listaLemb = new List<Lembrete>();

                Lembrete l1 = new Lembrete();
                l1.LembreteId = 1;
                l1.Data = DateTime.Today;
                l1.Conteudo = "Ablablablablablablablablablablablablablablablablablablablablablablablabla";
                listaLemb.Add(l1);

                Lembrete l2 = new Lembrete();
                l2.LembreteId = 2;
                l2.Data = DateTime.Today;
                l2.Conteudo = "Clauclaulcau";
                listaLemb.Add(l2);

                ViewBag.ListLembrete = listaLemb;
                return View();
           // }
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