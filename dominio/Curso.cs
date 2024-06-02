using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class Curso
    {
        public int Id { get; set; }

        public Usuario IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public decimal Costo { get; set; }

        public List<Categoria> Categorias { get; set; }

        public List<Capitulo> Capitulos { get; set; }

        public List<string> Etiquetas { get; set; }

        // TODO : ver si queremos cargar la imagen en al base de datos: https://stackoverflow.com/a/67361253
        public byte[] UrlImagen { get; set; }

        public bool ComentariosHabilitados { get; set; }

        // el usuario puede marcar su curso como no disponible
        public bool Disponible { get; set; }

        // si un admin elimina, se marca como inactivo
        public bool Activo { get; set; }

        public string NombresCategorias
        {
            get
            {
                string s = "";
                for (int i = 0; i < Categorias.Count; i++)
                {
                    s += Categorias[i].Nombre;
                    if (i == Categorias.Count - 1)
                        break;

                    s += ", ";

                }
                return s;
            }
        }

    }
}