using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Controllers.courses
{
    public class courseObject
    {
        public curso getCurso()
        {
            curso objCurso = new curso();

            return objCurso;
        }
        private List<disciplina> getDisciplina(int id)
        {
            List<disciplina> listaDisciplinas = new List<disciplina>;
            
            return listaDisciplinas;
        }
    }
    public class curso
    {
        int turno;
        List<disciplina> disciplinas;
    }
    sealed class disciplina
    {
        public int id;
        public int semestre;
        public String nome;
        public String sigla;
        public int quantidadeAulasMinistrada;
        public int sala;
    }
}