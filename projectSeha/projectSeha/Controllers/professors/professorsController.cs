using ProjectSeha.Entity;
using ProjectSeha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class professorsController : Controller
    {
        // GET: professors
        public ActionResult Index()
        {
            ProfessorModel model = new ProfessorModel();
            List<Professor> lista = model.Read();
            return View(lista);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Professor p = new Professor();

            p.Nome = form["Nome"];
            p.NomeGuerra = form["NomeGuerra"];
            p.Email = form["Email"];
            p.Senha = form["Senha"];
            p.ProfessorExiste = (form["ProfessorExiste"] == "on");

            ProfessorModel model = new ProfessorModel();
            model.Create(p);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ProfessorModel model = new ProfessorModel();
            model.Delete(id);
            return RedirectToAction("Index");
        }

    }
}