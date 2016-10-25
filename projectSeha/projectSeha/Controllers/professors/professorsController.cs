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

        public ActionResult Create()
        {
            return View();
        }
    }
}