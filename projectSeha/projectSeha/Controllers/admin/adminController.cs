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

        [HttpPost]
        public ActionResult CreateLemb(FormCollection form)
        {
            Lembrete l = new Lembrete();

            l.Conteudo = form["Lembrete"];
            l.Data = DateTime.Today;

            using (LembreteModel model = new LembreteModel())
            {
                model.Create(l);
                return RedirectToAction("semesters", "admin");
            }
        }

        [HttpGet]
        public ActionResult DeleteLemb(int id)
        {
            using (LembreteModel model = new LembreteModel())
            {
                model.Delete(id);
                return RedirectToAction("semesters", "admin");
            }
        }

        public ActionResult Steps()
        {
            using(ProfessorModel model = new ProfessorModel())
            {
                List<Professor> lista = model.Read();
                ViewBag.ListProfessor = lista;
                return View();
            }
        }

        public ActionResult _StepsAvailability(int ProfessorId)
        {
            Professor p;
            List<Disponibilidade> lista;

            using (ProfessorModel model = new ProfessorModel())
            {
                p = model.Read(ProfessorId);
            }
            using (AvailabilityModel model = new AvailabilityModel())
            {
                try
                {
                    lista = model.Read(p.PessoaId);
                }
                catch
                {
                    lista = null;
                }
            }
            ViewBag.ListDisponibilidade = lista;
            return PartialView(p);
        }

        public ActionResult results()
        {
            return View();
        }

    }
}