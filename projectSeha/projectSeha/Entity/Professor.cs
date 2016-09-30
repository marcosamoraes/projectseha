using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Entity
{
    public class Professor : Pessoa
    {
        public int CodPessoa { get; set; }
        public string NomeGuerra { get; set; }
        public int HorasAula { get; set; }
        public bool ExisteProfessor { get; set; }
        public bool InativaProfessor { get; set; }
        public string Observacoes { get; set; }
    }
}