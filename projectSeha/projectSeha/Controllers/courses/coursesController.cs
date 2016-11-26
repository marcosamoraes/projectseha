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
        static string msg;
        static bool ctrlMsg = false;

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                if (!disciplinaModel.DeleteAll(id))
                {
                    ctrlMsg = true;
                    msg = "Could not remove this course because it has some assigned disciplines";
                    return RedirectToAction("Index");
                }
            }
            using (CursoModel cursoModel = new CursoModel())
            {
                cursoModel.Delete(id);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (ctrlMsg)
            {
                ctrlMsg = false;
                ViewBag.Erro = msg;
            }
            else
            {
                ViewBag.Erro = null;
            }

            Session["dadosCurso"] = null;
            Session["ListaDisciplinas"] = null;
            using (CursoModel model = new CursoModel())
            {
                List<Curso> lista = model.Read();
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (ctrlMsg)
            {
                ctrlMsg = false;
                ViewBag.Erro = msg;
            }
            else
            {
                ViewBag.Erro = null;
            }
            if (Session["dadosCurso"] != null)
            {
                Curso curso = Session["dadosCurso"] as Curso;
                ViewBag.TituloCurso = curso.Titulo;
                ViewBag.TurnoCurso = curso.Turno;
            }
            return View();
        }

        [HttpPost]
        public ActionResult GoToDisciplina(FormCollection formCurso)
        {
            using (CursoModel model = new CursoModel())
            {
                if (model.VerificaCurso(formCurso["Titulo"].ToUpper(), formCurso["Turno"].ToUpper()))
                {
                    ctrlMsg = true;
                    msg = "This course already exists";
                    return RedirectToAction("create");
                }
                else
                {
                    Curso objCurso = new Curso();
                    objCurso.Titulo = formCurso["Titulo"];
                    objCurso.Turno = formCurso["Turno"];
                    Session["dadosCurso"] = objCurso;

                    return RedirectToAction("MenuDisciplinas");
                }
            }
        }


        [HttpGet]
        public ActionResult MenuDisciplinas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateDisciplina()
        {

            return View();
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

        [HttpGet]
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

        [HttpPost]
        public ActionResult UpdateDisciplinaLista(int id, FormCollection formulario)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];

            listaDisciplinas[id].Nome = formulario["TituloDisciplina"];
            listaDisciplinas[id].Sigla = formulario["SiglaDisciplina"];
            listaDisciplinas[id].Semestre = Convert.ToInt32(formulario["Periodo"]);
            listaDisciplinas[id].QtdAulas = Convert.ToInt32(formulario["QtdAulasMinistradas"]);

            return RedirectToAction("UpdateDisciplinas");
        }

        [HttpPost]
        public ActionResult AtualizarDisciplinaLista(int id, FormCollection formulario)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];

            listaDisciplinas[id].Nome = formulario["TituloDisciplina"];
            listaDisciplinas[id].Sigla = formulario["SiglaDisciplina"];
            listaDisciplinas[id].Semestre = Convert.ToInt32(formulario["Periodo"]);
            listaDisciplinas[id].QtdAulas = Convert.ToInt32(formulario["QtdAulasMinistradas"]);

            return RedirectToAction("MenuDisciplinas");
        }

        [HttpGet]
        public ActionResult DeleteDisciplina(int id)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];
            listaDisciplinas.RemoveAt(id);
            return RedirectToAction("MenuDisciplinas");
        }

        [HttpGet]
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

        public int GetUltimoCursoArmazenado()
        {
            using (CursoModel cursoModel = new CursoModel())
            {
                int idUltimoCurso = cursoModel.GetUltimoCursoArmazenado();
                return idUltimoCurso;
            }
        }

        public void CreateCursoBanco(Curso curso)
        {
            using (CursoModel cursoModel = new CursoModel())
            {
                cursoModel.Create(curso);
            }
        }

        /* -----------------------------Update-----------------------------------------*/


        [HttpPost]
        public ActionResult AddDisciplinaListaUpdate(FormCollection formulario)
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
            return RedirectToAction("UpdateDisciplinas");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            Curso CursoDados = GetCursoBanco(id);
            List<Disciplina> listaDisciplinas = GetListaDisciplinasBanco(id);

            Session["dadosCurso"] = CursoDados;
            Session["ListaDisciplinas"] = listaDisciplinas;

           
            return RedirectToAction("UpdateCurso");
        }

        public Curso GetCursoBanco(int CursoId)
        {
            using (CursoModel model = new CursoModel())
            {
                Curso curso = model.Read(CursoId);
                return curso;
            }
        }

        public List<Disciplina> GetListaDisciplinasBanco(int CursoId)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                return disciplinaModel.ReadDisciplinas(CursoId);
            }
        }

        [HttpGet]
        public ActionResult UpdateCurso()
        {
            if (ctrlMsg)
            {
                ctrlMsg = false;
                ViewBag.Erro = msg;
            }
            else
            {
                ViewBag.Erro = null;
            }
            Curso objCurso = (Curso)Session["dadosCurso"];
            ViewBag.CursoTitulo = objCurso.Titulo;
            ViewBag.CursoTurno = objCurso.Turno;
            return View();
        }

        [HttpPost]
        public ActionResult GoToUpdateDisciplina(FormCollection formCurso)
        {
            Curso sessaoCurso = (Curso)Session["dadosCurso"];
            Curso objCurso = new Curso();
            objCurso.CursoId = sessaoCurso.CursoId;
            objCurso.Titulo = formCurso["Titulo"];
            objCurso.Turno = formCurso["Turno"];
            Session["dadosCurso"] = objCurso;


            //-- alterações ----------

            using (CursoModel model = new CursoModel())
            {
                if (model.VerificaCurso(formCurso["Titulo"].ToUpper(), formCurso["Turno"].ToUpper(), sessaoCurso.CursoId))
                {
                    ctrlMsg = true;
                    msg = "This course already exists";
                    return RedirectToAction("UpdateCurso");
                }
                else
                {
                    return RedirectToAction("UpdateDisciplinas");
                }
            }
            //---------------------------------
            //return RedirectToAction("UpdateDisciplinas");
        }

        [HttpGet]
        public ActionResult UpdateDisciplinas()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult CriarDisciplina()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeleteDisciplinaUpdate(int id)
        {
            var listaDisciplinas = (List<Disciplina>)Session["ListaDisciplinas"];
            var tamanhoLista = listaDisciplinas.Count();
            int posicaoLista = id;
            int idDisciplina = 0;

            for (int i = 0; i < tamanhoLista; i++)
            {
                var objDisciplina = listaDisciplinas[i];
                if (i == posicaoLista)
                {
                    idDisciplina = objDisciplina.DisciplinaId;
                    break;
                }

            }

            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                if (disciplinaModel.VerificaAtribuicao(idDisciplina))
                {
                    ctrlMsg = true;
                    msg = "You can not remove this discipline because it is assigned";
                    return RedirectToAction("UpdateDisciplinas");
                }
            }
            listaDisciplinas.RemoveAt(posicaoLista);
            return RedirectToAction("UpdateDisciplinas");
        }

        [HttpGet]
        public ActionResult AtualizarDisciplina(int id)
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

        public ActionResult Atualizar()
        {
            Curso objCursoInterface = (Curso)Session["dadosCurso"];
            List<Disciplina> listaDisciplinasInterface = (List<Disciplina>)Session["ListaDisciplinas"];
            List<Disciplina> listaDisciplinasBanco = GetListaDisciplinasBanco(objCursoInterface.CursoId);

            UpdateCurso(objCursoInterface);

            List<int> listaDisciplinasJaExistentesInterfaceId = GetListaDisciplinaJaExistentes(listaDisciplinasInterface);
            AtualizarDisciplinasBanco(listaDisciplinasJaExistentesInterfaceId, listaDisciplinasInterface);

            DeletarDisciplinas(listaDisciplinasInterface, listaDisciplinasBanco);

            List<Disciplina> listaDisciplinasNovas = GetListaDisciplinaNovas(listaDisciplinasInterface);
            AdicionarDisciplinas(objCursoInterface.CursoId, listaDisciplinasNovas);

            Session["dadosCurso"] = null;
            Session["ListaDisciplinas"] = null;
            return RedirectToAction("Index");
        }

        public List<int> GetListaDisciplinaJaExistentes(List<Disciplina> lista)
        {
            List<int> listaDisciplinasJaExistentesId = new List<int>();

            var tamanhoLista = lista.Count();
            for (int i = 0; i < tamanhoLista; i++)
            {
                if (lista[i].DisciplinaId != 0)
                {
                    listaDisciplinasJaExistentesId.Add(lista[i].DisciplinaId);
                }
            }

            return listaDisciplinasJaExistentesId;
        }

        public void DeletarDisciplinas(List<Disciplina> listaDisciplinasInterface, List<Disciplina> listaDisciplinasBanco)
        {
            List<int> listaDisciplinasJaExistentesInterfaceId = GetListaDisciplinaJaExistentes(listaDisciplinasInterface);
            List<int> listaDisciplinasBancoId = GetListaDisciplinaId(listaDisciplinasBanco);
            var disciplinasDel = listaDisciplinasBancoId.Except(listaDisciplinasJaExistentesInterfaceId).ToList();
            DeletarDisciplinas(disciplinasDel);
        }

        public void DeletarDisciplinas(List<int> listaId)
        {
            int tamanhoLista = listaId.Count();
            for (int i = 0; i < tamanhoLista; i++)
            {
                int id = listaId[i];
                ApagarDisciplinaBanco(id);
            }
        }

        public void ApagarDisciplinaBanco(int id)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                disciplinaModel.Delete(id);
            }
        }

        public List<Disciplina> GetListaDisciplinaNovas(List<Disciplina> lista)
        {
            List<Disciplina> listaDisciplinasNovas = new List<Disciplina>();

            var tamanhoLista = lista.Count();
            for (int i = 0; i < tamanhoLista; i++)
            {
                if (lista[i].DisciplinaId == 0)
                {
                    listaDisciplinasNovas.Add(lista[i]);
                }
            }
            return listaDisciplinasNovas;
        }

        public List<int> GetListaDisciplinaId(List<Disciplina> lista)
        {
            List<int> listaIds = new List<int>();

            var tamanhoLista = lista.Count();
            for (int i = 0; i < tamanhoLista; i++)
            {
                var idDisciplina = lista[i].DisciplinaId;
                listaIds.Add(idDisciplina);
            }

            return listaIds;
        }

        public void AdicionarDisciplinas(int CursoId, List<Disciplina> listaDisciplinasInterface)
        {
            int tamanhoListaObjs = listaDisciplinasInterface.Count();
            for (int j = 0; j < tamanhoListaObjs; j++)
            {
                Disciplina Disciplinanova = listaDisciplinasInterface[j];
                Disciplinanova.CodCurso = CursoId;
                CreateDisciplinaBanco(Disciplinanova);
            }

        }

        public void CreateDisciplinaBanco(Disciplina disciplina)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                disciplinaModel.Create(disciplina);
            }
        }

        public void AtualizarDisciplinasBanco(List<int> listaDisciplinasJaExistentesInterfaceId, List<Disciplina> listaDisciplinasInterface)
        {
            var tamanhoListaDisciplinasJaExistentesId = listaDisciplinasJaExistentesInterfaceId.Count();
            var tamanholistaDisciplinasInterface = listaDisciplinasInterface.Count();
            for (int i = 0; i < tamanhoListaDisciplinasJaExistentesId; i++)
            {
                int interfaceDisciplinaId = listaDisciplinasJaExistentesInterfaceId[i];
                for (int j = 0; j < tamanholistaDisciplinasInterface; j++)
                {
                    Disciplina objDisciplina = listaDisciplinasInterface[j];
                    if (objDisciplina.DisciplinaId == interfaceDisciplinaId)
                    {
                        UpdateDisciplinaBanco(objDisciplina);
                    }
                }
            }
        }

        public void UpdateDisciplinaBanco(Disciplina disciplina)
        {
            using (DisciplinaModel disciplinaModel = new DisciplinaModel())
            {
                disciplinaModel.Update(disciplina);
            }
        }

        public void UpdateCurso(Curso curso)
        {
            using (CursoModel model = new CursoModel())
            {
                model.Update(curso);
            }
        }
    }
}