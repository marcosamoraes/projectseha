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
            Pessoa e = (Pessoa)Session["professor"];
            return View(e);
        }

        public ActionResult _Availability(int ProfessorId)
        {
            Professor p;
            List<Disponibilidade> lista;

            List<int> listaBloqueados = new List<int>();
            int[] manha = { 1, 9, 17, 25, 33, 2, 10, 18, 26, 34, 3, 11, 19, 27, 35, 41, 42, 43, 46, 47, 48 };
            int[] tarde = { 4, 12, 20, 28, 36, 5, 13, 21, 29, 37, 6, 14, 22, 30, 38, 41, 42, 43, 46, 47, 48 };
            int[] noite = { 7, 15, 23, 31, 39, 8, 16, 24, 32, 40 };
            int[] sabado = { 44, 45 };

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

            using (AssignmentModel model = new AssignmentModel())
            {
                List<string> listaTurno = model.ReadTurno(ProfessorId);

                //Adiciona os id's dos slots da manhã para a lista de bloqueio
                if (!listaTurno.Contains("Morning"))
                {
                    foreach(var item in manha)
                    {
                        listaBloqueados.Add(item);
                    }
                }

                //Adiciona os id's dos slots da tarde para a lista de bloqueio
                if (!listaTurno.Contains("Afternoon"))
                {
                    foreach (var item in tarde)
                    {
                        listaBloqueados.Add(item);
                    }
                }

                //Adiciona os id's dos slots da noite para a lista de bloqueio
                if (!listaTurno.Contains("Evening"))
                {
                    foreach (var item in noite)
                    {
                        listaBloqueados.Add(item);
                    }
                    foreach (var item in sabado)
                    {
                        listaBloqueados.Add(item);
                    }
                }

            }
            
            ViewBag.ListBloqueados = listaBloqueados;
            ViewBag.ListDisponibilidade = lista;
            return View(p);
        }

        public void UpdateObservation(int ProfessorId, string observacoes)
        {
            using(ProfessorModel model = new ProfessorModel())
            {
                model.UpdateObservation(ProfessorId, observacoes);
            }
        }
    }
}