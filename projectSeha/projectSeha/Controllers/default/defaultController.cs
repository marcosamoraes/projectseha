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
                    ViewBag.Sucesso = "Alterado com sucesso!";
                }
                else
                {
                    ViewBag.Erro = "Informe as senhas corretamente";
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
                    ViewBag.Erro = "Usuário não reconhecido";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
           Session.RemoveAll();
           return RedirectToAction("Index");
        }
    }
}