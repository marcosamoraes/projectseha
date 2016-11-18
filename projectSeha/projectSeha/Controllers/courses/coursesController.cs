using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSeha.Entity;
using ProjectSeha.Models;

namespace ProjectSeha.Controllers
{
    [AutorizaAdmin]
    public class coursesController : Controller
    {
        // GET: courses
        public ActionResult Index()
        {
            using(CursoModel model = new CursoModel())
            {
                List<Curso> lista = model.Read();
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
            Curso cursoDados = new Curso();

            cursoDados.Titulo = form["Titulo"];
            cursoDados.Turno = form["Turno"];

            using (CursoModel cursoModel = new CursoModel())
            {
                cursoModel.Create(cursoDados);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (CursoModel cursoModel = new CursoModel())
            {
                cursoModel.Delete(id);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (CursoModel model = new CursoModel())
            {
                Curso curso = model.Read(id);
                return View(curso);
            }
        }

        [HttpPost]
        public ActionResult Update(int id, FormCollection form)
        {
            Curso curso = new Curso();
            curso.CursoId = id;
            curso.Titulo = form["Titulo"];
            curso.Turno = form["Turno"];

            using (CursoModel model = new CursoModel())
            {
                model.Update(curso);
                return RedirectToAction("Index");
            }
        }

        public ActionResult _CreateDisciplina()
        {
            return PartialView();
        }

        public ActionResult _TabDisciplinas()
        {
            return PartialView();
        }

        public List<Disciplina> listaDisciplinas = new List<Disciplina>();

        [HttpPost]
        public ActionResult AddDisciplinaLista(FormCollection form)
        {
            Disciplina newDisciplina = new Disciplina();
            newDisciplina.Nome = form["TituloDisciplina"];
            newDisciplina.Sigla = form["SiglaDisciplina"];
            newDisciplina.Semestre = Convert.ToInt32(form["Periodo"]);
            newDisciplina.QtdAulas = Convert.ToInt32(form["QtdAulasMinistradas"]);

            listaDisciplinas.Add(newDisciplina);
            ViewBag.listaDisciplinas = listaDisciplinas;
            return PartialView("_MenuDisciplinas");
        }

        public ActionResult _MenuDisciplinas()
        {
            return View();
        }
    }
}