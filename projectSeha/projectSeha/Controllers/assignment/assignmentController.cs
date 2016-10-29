using ProjectSeha.Entity;
using ProjectSeha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProjectSeha.Controllers.assignment;

namespace ProjectSeha.Controllers
{
    public class assignmentController : Controller
    {
        // GET: assignment
        public ActionResult Assignment()
        {
            using (ProfessorModel model = new ProfessorModel())
            {
                List<Professor> listaProf = model.Read();
                ViewBag.ListProfessor = listaProf;
                return View();
            }
        }

        public ActionResult _AssignmentProfessor(int ProfessorId)
        {
            Professor p;
            List<Curso> listaCurso;

            using (ProfessorModel model = new ProfessorModel())
            {
                p = model.Read(ProfessorId);
            }
            using (CursoModel model = new CursoModel())
            {
                listaCurso = model.Read();
            }

            ViewBag.ListCurso = listaCurso;
            return PartialView(p);
        }

        public ActionResult _AssignmentCurso(int CursoId)
        {
            List<Disciplina> lista;
            using (DisciplinaModel model = new DisciplinaModel())
            {
                lista = model.Read(CursoId);
            }
            return PartialView(lista);
        }
    }
}