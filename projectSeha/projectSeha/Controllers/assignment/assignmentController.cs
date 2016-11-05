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
        List<Atribuicao> listaAtribuicao = new List<Atribuicao>();
       
        // Carrega a lista de professores
        public ActionResult Assignment()
        {
            using (ProfessorModel model = new ProfessorModel())
            {
                List<Professor> listaProf = model.Read();
                ViewBag.ListProfessor = listaProf;
                return View();
            }
        }

        //Carrega a lista de cursos em uma Partial View e alguns dados do professor selecionado
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

        public ActionResult _AssignmentCurso(int CursoId, int ProfessorId) //possivelmente fazer viewmodel
        {
            //Carregar a atribuicao do professor também
            listaAtribuicao.Clear();
            List<Disciplina> listaDisciplina;

            using(AssignmentModel model = new AssignmentModel())
            {
                listaAtribuicao = model.Read(ProfessorId);
            }
            using (DisciplinaModel model = new DisciplinaModel())
            {
                listaDisciplina = model.Read(CursoId);
            }

            ViewBag.ListAtribuicao = listaAtribuicao;
            return PartialView(listaDisciplina);
        }

        public JsonResult Create(int ProfessorId, string disciplinas) //possivelmente fazer viewmodel
        {
            string[] valores = disciplinas.Split(',');
            for (int i = 0; i < valores.Length; i++)
            {
                Atribuicao a = new Atribuicao();
                a.CodProfessor = ProfessorId;
                a.CodDisciplina = Convert.ToInt32(valores[i]);

                using (AssignmentModel model = new AssignmentModel()
                {
                    model.Create(a);
                }
            }
            return Json("Cadastrado com sucesso!");
        }







            //Métodos para adicionar e remover itens da listaAtribuição temporária

        }
}