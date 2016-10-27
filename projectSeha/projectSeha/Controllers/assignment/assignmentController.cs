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
            dynamicTable dynamicTable = new dynamicTable();
            ViewBag.DadosTabelaDinamica = dynamicTable.getCurso();

            List<Professor> listaProf;
            List<Curso> listaCurso;

            using (ProfessorModel model = new ProfessorModel())
            {
                listaProf = model.Read();
            }
            using (CursoModel model = new CursoModel())
            {
                listaCurso = model.Read();
            }

            ViewBag.ListCurso = listaCurso;
            ViewBag.ListProfessor = listaProf;

            return View();            
        }
    }
}