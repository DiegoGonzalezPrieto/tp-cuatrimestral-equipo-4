using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;


namespace webform
{
    public partial class VerCurso : System.Web.UI.Page
    {
        public Curso curso { get; set; } = new Curso();
        public Capitulo capitulo { get; set; } = new Capitulo();
        public Contenido contenido { get; set; } = new Contenido();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carga Inicial

                obtenerIdsContenido();
                obtenerDatos();




            }
        }

        private void obtenerIdsContenido()
        {
            try
            {
                int idCurso = int.Parse(Request.QueryString["curso"]);
                curso.Id = idCurso;
            }
            catch (Exception)
            {
                Session.Add("error", "El curso solicitado no existe.");
                Response.Redirect("Error.aspx", true);
            }

            if (Request.QueryString["capitulo"] != null)
            {
                short ordenCapitulo = short.Parse(Request.QueryString["capitulo"]);
                capitulo.Orden = ordenCapitulo;
            }
            else
            {
                capitulo.Orden = 1;
                contenido.Orden = 1;
                return;
            }

            if (Request.QueryString["contenido"] != null)
            {
                short ordenContenido = short.Parse(Request.QueryString["contenido"]);
                contenido.Orden = ordenContenido;
            }
            else
            {
                contenido.Orden = 1;
            }
        }
        private void obtenerDatos()
        {
            try
            {
                curso = CursoNegocio.obtenerCurso(curso.Id);
                if (curso.Id == 0)
                    throw new Exception();

                capitulo = CapituloNegocio.obtenerCapituloDeCurso(curso.Id, capitulo.Orden);
                if (capitulo.Id == 0)
                    throw new Exception();

                contenido = ContenidoNegocio.obtenerContenidoDeCapitulo(capitulo.Id, contenido.Orden);
                if (contenido.Id == 0)
                    throw new Exception();

            }
            catch (Exception)
            {

                Session.Add("error", "No se encuentra el contenido solicitado.");
                Response.Redirect("Error.aspx", false);
            }


        }
    }
}
