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

        public ActionResult About()//Login
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)//Login
        {
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
                
            }
            ViewBag.Mensagem = "Usuário não reconhecido";
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            //Session.Remove("professor");//professor ou admin
            //Session.Remove("admin");
            //Session.Abandon();

            return RedirectToAction("Index");
        }
    }
}