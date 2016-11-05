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

        public ActionResult _AssignmentCurso(int CursoId, int ProfessorId) //passível de alteração (CursoId presente na atribuição)
        {
            //Carregar a atribuicao do professor também
            List<Atribuicao> listaAtribuicao;
            List<Atribuicao> listaDisabled;
            List<Disciplina> listaDisciplina;

            using (AssignmentModel model = new AssignmentModel())
            {
                listaAtribuicao = model.Read(ProfessorId);
            }
            using (AssignmentModel model = new AssignmentModel())
            {
                listaDisabled = model.ReadDisabled(ProfessorId);
            }
            using (DisciplinaModel model = new DisciplinaModel())
            {
                listaDisciplina = model.Read(CursoId);
            }
        
            ViewBag.ListAtribuicao = listaAtribuicao;
            ViewBag.ListDisabled = listaDisabled;
            return PartialView(listaDisciplina);
        }

        public ActionResult Create(int ProfessorId, int CursoId, string disciplinas) //possivelmente fazer viewmodel
        {
            using (AssignmentModel model = new AssignmentModel())
            {
                model.Delete(ProfessorId, CursoId);
            }

            string[] valores = disciplinas.Split(',');
            if (valores[0] != "")
            {
                for (int i = 0; i < valores.Length; i++)
                {
                    Atribuicao a = new Atribuicao();
                    a.CodProfessor = ProfessorId;
                    a.CodDisciplina = Convert.ToInt32(valores[i]);
                    a.CodCurso = CursoId;

                    using (AssignmentModel model = new AssignmentModel())
                    {
                        model.Create(a);
                    }
                }
                return Json("Cadastrado com sucesso!");
            }
            else
                return Json("Nenhum item foi cadastrado");
        }

    }
}