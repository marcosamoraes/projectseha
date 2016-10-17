using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Entity
{
    public class Disciplina 
    {
        public int DisciplinaId { get; set; }
        public int CodCurso { get; set; }
        public string Nome { get; set; }
        public int QtdAulas { get; set; }
        public int Semestre { get; set; }
        public string Sigla { get; set; }
    }
}