using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class DenunciaCurso
    {
        public int Id { get; set; }

        public int IdCurso { get; set; }

        public int IdDenunciante { get; set; }

        public string MensajeDenuncia { get; set; }
        
        public bool Resuelta { get; set; }
    }
}