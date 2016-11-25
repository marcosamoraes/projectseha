using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Entity
{
    public class Dashboard
    {
        public class CountProf_Discp
        {
            public string Nome { get; set; }
            public int QtdDisciplinas { get; set; }
        }

        public class CountProf_Curso
        {
            public string Nome { get; set; }
            public int QtdCursos { get; set; }
        }

        public class CountCurso_Discp
        {
            public string Titulo { get; set; }
            public string Turno { get; set; }
            public int QtdDisciplinas { get; set; }
        }

        //Classes do availability
        public class CountProf_Available
        {
            public int CodProfessor;
            public string NomeGuerra;
        }

        public class CountProf_Slot
        {
            public int CodProfessor;
            public string HoraInicio;
            public bool status_slot;
        }
    }
}