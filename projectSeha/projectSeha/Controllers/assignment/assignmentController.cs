﻿using ProjectSeha.Entity;
using ProjectSeha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace ProjectSeha.Controllers
{
    [AutorizaAdmin]
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

        public ActionResult Create(int ProfessorId, int CursoId, int QtdAulas, string disciplinas)
        {
            using (AssignmentModel model = new AssignmentModel())
            {
                model.Delete(ProfessorId, CursoId);
            }

            using (ProfessorModel model = new ProfessorModel())
            {
                model.UpdateHorasAula(ProfessorId, QtdAulas);
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
            }

            using(AvailabilityModel model = new AvailabilityModel())
            {
                model.Delete(ProfessorId);//apaga a disponibilidade do professor já que a assignments foi alterada
            }

            return Json("Salvo com sucesso");
        }
    }
}