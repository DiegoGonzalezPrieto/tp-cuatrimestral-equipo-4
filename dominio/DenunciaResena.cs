using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class DenunciaResena
    {
        public int Id { get; set; }

        public int IdReseña { get; set; }

        public int IdDenunciante { get; set; }

        public string MensajeDenuncia { get; set; }

        public bool Resuelta { get; set; }
    }
}