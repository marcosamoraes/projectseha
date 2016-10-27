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

            List<Professor> lista;

            using (ProfessorModel model = new ProfessorModel())
            {
                 lista = model.Read();
            }
            ViewBag.ListProfessor = new SelectList(lista, "PessoaId", "Nome");
            return View();            
        }
    }
}