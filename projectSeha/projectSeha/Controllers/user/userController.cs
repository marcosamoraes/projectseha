using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSeha.Entity;
using ProjectSeha.Models;

namespace ProjectSeha.Controllers
{
   
    public class userController : Controller
    {
        // GET: user
        public ActionResult Availability()
        {
            Pessoa e = (Pessoa)Session["professor"];
            return View(e);
        }

        public ActionResult _Availability(int ProfessorId)
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
            using (LembreteModel model = new LembreteModel())
            {
                List<Lembrete> listaLemb = model.Read();
                ViewBag.ListLembrete = listaLemb;
            }

            ViewBag.ListDisponibilidade = lista;
            return View(p);
        }

        public void UpdateObservation(int ProfessorId, string observacoes)
        {
            using(ProfessorModel model = new ProfessorModel())
            {
                model.UpdateObservation(ProfessorId, observacoes);
            }
            //return Json("Observação inserida");
        }
    }
}