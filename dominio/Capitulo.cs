using System;
using System.Collections.Generic;

namespace dominio
{
    public class Capitulo
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }

        public string Nombre { get; set; }

        public short Orden { get; set; }

        public List<Contenido> Contenidos { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }

        public bool Liberado { get; set; }
    }
}