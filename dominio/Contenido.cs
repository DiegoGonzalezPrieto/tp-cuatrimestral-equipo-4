using System;

namespace dominio
{
    public class Contenido
    {
        public int Id { get; set; }
        public int IdCapitulo { get; set; }

        public string Nombre { get; set; }

        public short Orden { get; set; }

        public TipoContenido Tipo { get; set; }

        public string Texto { get; set; }

        public string UrlVideo { get; set; }

        // TODO : ver cómo guardar y obtener pdf de la base de datos
        // se podria guarda el link de alojamiento (ej: dropbox) y poner un link en la pagina con un
        // nombre del capitulo
        public byte[] Archivo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Activo { get; set; }
        public bool Liberado { get; set; }
    }
}