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
            using (CursoModel model = new CursoModel())
            {
                List<Curso> lista = model.Read();
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["dadosCurso"] != null)
            {
                Curso curso = Session["dadosCurso"] as Curso;
                ViewBag.TituloCurso = curso.Titulo;
                ViewBag.TurnoCurso = curso.Turno;
            }
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
            Curso Curso = GetCursoBanco(id);
            List<Disciplina> listaDisciplinas = GetListaDisciplinasBanco(id);
            return RedirectToAction("UpdateCurso");
        }

        public List<Disciplina> GetListaDisciplinasBanco(int CursoId)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                return disciplinaModel.Read(CursoId);
            }
        }

        public Curso GetCursoBanco(int CursoId)
        {
            using (CursoModel model = new CursoModel())
            {
                Curso curso = model.Read(CursoId);
                return curso;
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

        [HttpPost]
        public ActionResult AddDisciplinaLista(FormCollection formulario)
        {
            List<Disciplina> listaDisciplinas = new List<Disciplina>();
            if (Session["ListaDisciplinas"] != null)
            {
                listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];
            }

            Disciplina newDisciplina = new Disciplina();
            newDisciplina.Nome = formulario["TituloDisciplina"];
            newDisciplina.Sigla = formulario["SiglaDisciplina"];
            newDisciplina.Semestre = Convert.ToInt32(formulario["Periodo"]);
            newDisciplina.QtdAulas = Convert.ToInt32(formulario["QtdAulasMinistradas"]);
            listaDisciplinas.Add(newDisciplina);

            Session["ListaDisciplinas"] = listaDisciplinas;
            return RedirectToAction("MenuDisciplinas");
        }

        public ActionResult MenuDisciplinas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GoToDisciplina(FormCollection formCurso)
        {
            Curso objCurso = new Curso();
            objCurso.Titulo = formCurso["Titulo"];
            objCurso.Turno = formCurso["Turno"];
            Session["dadosCurso"] = objCurso;

            return RedirectToAction("MenuDisciplinas");
        }

        public ActionResult CreateDisciplina()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteDisciplina(int id)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];
            listaDisciplinas.RemoveAt(id);
            return RedirectToAction("MenuDisciplinas");
        }

        public ActionResult UpdateDisciplina(int id)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];
            var objDisciplina = listaDisciplinas[id];
            ViewBag.IndiceLista = id;
            ViewBag.Semestre = objDisciplina.Semestre;
            ViewBag.Titulo = objDisciplina.Nome;
            ViewBag.Sigla = objDisciplina.Sigla;
            ViewBag.QtdAulas = objDisciplina.QtdAulas;
            return View();
        }

        public ActionResult UpdateDisciplinaLista(int id, FormCollection formulario)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];
            //var objDisciplina = listaDisciplinas[id];

            listaDisciplinas[id].Nome = formulario["TituloDisciplina"];
            listaDisciplinas[id].Sigla = formulario["SiglaDisciplina"];
            listaDisciplinas[id].Semestre = Convert.ToInt32(formulario["Periodo"]);
            listaDisciplinas[id].QtdAulas = Convert.ToInt32(formulario["QtdAulasMinistradas"]);

            return RedirectToAction("MenuDisciplinas");
        }

        public ActionResult Salvar()
        {
            var curso = (Curso)Session["dadosCurso"];
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];

            CreateCursoBanco(curso);
            int idUltimoCursoSalvo = GetUltimoCursoArmazenado();

            var tamanhoLista = listaDisciplinas.Count();
            for (int i = 0; i < tamanhoLista; i++)
            {
                var disciplina = listaDisciplinas[i];
                disciplina.CodCurso = idUltimoCursoSalvo;
                CreateDisciplinaBanco(disciplina);
            }
            Session["dadosCurso"] = null;
            Session["ListaDisciplinas"] = null;
            return RedirectToAction("Index");
        }

        public void CreateCursoBanco(Curso curso)
        {
            using (CursoModel cursoModel = new CursoModel())
            {
                cursoModel.Create(curso);
            }
        }

        public void CreateDisciplinaBanco(Disciplina disciplina)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                disciplinaModel.Create(disciplina);
            }
        }

        public int GetUltimoCursoArmazenado()
        {
            using (CursoModel cursoModel = new CursoModel())
            {
                int idUltimoCurso = cursoModel.GetUltimoCursoArmazenado();
                return idUltimoCurso;
            } 
        }
    }
}