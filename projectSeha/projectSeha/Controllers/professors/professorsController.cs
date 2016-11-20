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
    public class professorsController : Controller
    {
        static bool ctrlDelete;
        // GET: professors
        public ActionResult Index()
        {
            using (ProfessorModel model = new ProfessorModel())
            {
                List<Professor> lista = model.Read();
                if (ctrlDelete)
                {
                    ctrlDelete = false;
                    ViewBag.Erro = "Não foi possível remover este professor pois ele possui vínculo com as Disciplinas";
                }
                return View(lista);
            }
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
            p.ProfessorExiste = (form["ProfessorExiste"] == "on");

            using (ProfessorModel model = new ProfessorModel())
            {
                if (model.Create(p))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Erro = "Verifique se o nome ou o email do professor já existem";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (ProfessorModel model = new ProfessorModel())
            {
                if (model.Delete(id))
                {
                    ctrlDelete = false;
                    return RedirectToAction("Index");
                }
                else
                {
                    ctrlDelete = true;
                    return RedirectToAction("Index");
                }
                
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (ProfessorModel model = new ProfessorModel())
            {
                Professor p = model.Read(id);
                return View(p);
            }
        }

        [HttpPost]
        public ActionResult Update(int id, FormCollection form)
        {
            Professor p = new Professor();
            p.PessoaId = id;
            p.Nome = form["Nome"];
            p.NomeGuerra = form["NomeGuerra"];
            p.Email = form["Email"];
            p.ProfessorExiste = (form["ProfessorExiste"] == "on");
            p.ProfessorAtivo = (form["ProfessorAtivo"] == "on");

            using (ProfessorModel model = new ProfessorModel())
            {
                if (model.Update(p))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Erro = "Verifique se o nome ou o email do professor já existem";
                    return View(p);
                }
            }
        }

    }
}