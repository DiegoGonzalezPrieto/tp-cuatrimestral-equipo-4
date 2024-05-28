using System.Collections.Generic;

namespace dominio
{
    public class Capitulo
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Orden { get; set; }

        public List<Contenido> Contenidos { get; set; }

        public bool Liberado { get; set; }
    }
}