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
    }
}