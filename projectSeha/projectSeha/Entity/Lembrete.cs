using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Entity
{
    public class Lembrete
    {
        public int LembreteId { get; set; }
        public DateTime Data { get; set; }
        public string Conteudo { get; set; }
    }
}