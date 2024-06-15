using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Comentario
    {
        public int Id {  get; set; }
        public int IdCurso { get; set; }

        public int IdUsuario { get; set; }

        public string Mensaje { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }

        public int Id_aResponder { get; set; }

        public string NombreUsuario { get; set; }

    }
}