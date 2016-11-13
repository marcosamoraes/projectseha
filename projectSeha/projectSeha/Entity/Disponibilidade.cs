using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectSeha.Entity
{
    public class Disponibilidade
    {
        public int CodProfessor { get; set; }
        public int CodSlot { get; set; }
        public bool Status_slot { get; set; }
    }
}