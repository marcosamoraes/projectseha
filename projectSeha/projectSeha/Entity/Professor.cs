using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Entity
{
    public class Professor : Pessoa
    {
        public string NomeGuerra { get; set; }
        public int HorasAula { get; set; }
        public bool ProfessorExiste { get; set; }
        public bool ProfessorAtivo { get; set; }
        public string Observacoes { get; set; }
    }
}