using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSeha.Entity;
using ProjectSeha.Models;

namespace ProjectSeha.Controllers
{
    [AutorizaProfessor]
    public class userController : Controller
    {
        // GET: user
        public ActionResult Availability()
        {
            Pessoa e = (Pessoa) Session["professor"];
            Professor p;
            List<Disponibilidade> lista;

            using (ProfessorModel model = new ProfessorModel())
            {
                p = model.Read(e.PessoaId);
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

        public ActionResult Create(int ProfessorId, string slotDisponivel, string slotTalvez)
        {
            using (AvailabilityModel model = new AvailabilityModel())
            {
                model.Delete(ProfessorId);
            }

            string[] disponiveis = slotDisponivel.Split(',');
            if (disponiveis[0] != "")
            {
                for (int i = 0; i < disponiveis.Length; i++)
                {
                    Disponibilidade d = new Disponibilidade();
                    d.CodProfessor = ProfessorId;
                    d.CodSlot = Convert.ToInt32(disponiveis[i]);
                    d.Status_slot = true; //Status_slot True para slots verdes

                    using (AvailabilityModel model = new AvailabilityModel())
                    {
                        model.Create(d);
                    }
                }
            }

            string[] talvez = slotTalvez.Split(',');
            if (talvez[0] != "")
            {
                for (int i = 0; i < talvez.Length; i++)
                {
                    Disponibilidade d = new Disponibilidade();
                    d.CodProfessor = ProfessorId;
                    d.CodSlot = Convert.ToInt32(talvez[i]);
                    d.Status_slot = false; //Status_slot false para slots lranjas

                    using (AvailabilityModel model = new AvailabilityModel())
                    {
                        model.Create(d);
                    }
                }
            }

            return Json("Salvo com sucesso");
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