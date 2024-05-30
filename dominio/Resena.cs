using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Web;

namespace dominio
{
    public class Resena
    {
        public int Id { get; set; }

        public int IdCurso { get; set; }

        public int IUsuario { get; set; }

        public int Puntaje { get; set; }

        public string Mensaje { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activa { get; set; }
    }
}