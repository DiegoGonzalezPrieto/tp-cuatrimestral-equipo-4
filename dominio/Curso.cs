using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace dominio
{
    public class Curso
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public decimal Costo { get; set; }

        public List<Categoria> Categorias { get; set; }

        public List<Capitulo> Capitulos { get; set; }

        public List<string> Etiquetas { get; set; }

        // TODO : ver si queremos cargar la imagen en al base de datos: https://stackoverflow.com/a/67361253
        public byte[] UrlImagen { get; set; }
        public string ImagenDataUrl
        {
            get
            {
                if (UrlImagen != null)
                {
                    string tipoImagen = "image/jpeg";
                    if (UrlImagen.Length >= 4 && UrlImagen[0] == 0x3C)
                    {
                        tipoImagen = "image/svg+xml";
                    }
                    string base64String = Convert.ToBase64String(UrlImagen);
                    string dataUrl = $"data:{tipoImagen};base64,{base64String}";
                    return dataUrl;
                }
                return null;
            }
        }

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

        public Indice Indice { get; set; }

    }

    public struct Indice
    {
        public List<CapituloIndice> Capitulos { get; set; }
    }

    public struct CapituloIndice
    {
        public string Nombre { get; set; }
        public short Orden {  get; set; }
        public List<ContenidoIndice> Contenidos { get; set; }

    }
    public struct ContenidoIndice
    {
        public int Id{ get; set; }
        public string Nombre{ get; set; }
        public short Orden{ get; set; }
        public bool Completado { get; set; }

    }
}