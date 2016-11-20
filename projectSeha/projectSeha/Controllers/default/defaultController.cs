using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSeha.Models;
using ProjectSeha.Entity;

namespace ProjectSeha.Controllers
{

    public class defaultController : Controller
    {
        public ActionResult Index()//Login
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Password()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Password(FormCollection form)
        {
            int PessoaId = Convert.ToInt32(form["PessoaId"]);
            string senhaAntiga = form["senhaAntiga"];
            string senhaNova = form["senhaNova"];
            string confirmaSenha = form["confirmaSenha"];
            Pessoa p;

            using (PessoaModel model = new PessoaModel())
            {
                p = model.Read(PessoaId);
            }

            using(PessoaModel model = new PessoaModel())
            {
                if (p.Senha == senhaAntiga && senhaNova == confirmaSenha)
                {
                    model.UpdatePassword(PessoaId, senhaNova);
                    ViewBag.Sucesso = "Saved successfully";
                }
                else
                {
                    ViewBag.Erro = "Enter passwords correctly";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)//Login
        {
            Session.RemoveAll();

            string email = form["usuario"];
            string senha = form["password"];

            using (PessoaModel model = new PessoaModel())
            {
                Pessoa e = model.Login(email, senha);

                if (e != null)
                {
                    if (e.Permissao_admin)
                    {
                        Session["admin"] = e;
                        return RedirectToAction("semesters", "admin");
                    }
                    else
                    {
                        Session["professor"] = e;
                        return RedirectToAction("availability", "user");
                    }
                }
                else
                {
                    ViewBag.Erro = "User not recognized";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
           Session.RemoveAll();
           return RedirectToAction("Index");
        }

        //tanto para admin quando para user
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

        public ActionResult error()
        {
            return View();
        }
    }
}