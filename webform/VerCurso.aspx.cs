using System;
using System.Collections.Generic;
using System.Data;
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
        public Contenido contenidoSiguiente { get; set; } = new Contenido();
        public Contenido contenidoAnterior { get; set; } = new Contenido();
        public string urlAnterior { get; set; }
        public string urlSiguiente { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            // Carga Inicial
            obtenerIdsContenido();
            obtenerDatos();
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


                contenidoAnterior = ContenidoNegocio.obtenerContenidoAnterior(contenido.Id);
                short ordenCapituloDelContenidoAnterior = contenidoAnterior.IdCapitulo == contenido.IdCapitulo
                    ? capitulo.Orden
                    : (short)(capitulo.Orden - 1);
                contenidoSiguiente = ContenidoNegocio.obtenerContenidoSiguiente(contenido.Id);
                short ordenCapituloDelContenidoSiguiente = contenidoSiguiente.IdCapitulo == contenido.IdCapitulo
                    ? capitulo.Orden
                    : (short)(capitulo.Orden + 1);

                if (contenidoSiguiente.Id != 0)
                    urlSiguiente = "VerCurso.aspx?curso=" + curso.Id +
                        "&capitulo=" + ordenCapituloDelContenidoSiguiente.ToString() +
                        "&contenido=" + contenidoSiguiente.Orden;

                if (contenidoAnterior.Id != 0)
                    urlAnterior = "VerCurso.aspx?curso=" + curso.Id +
                        "&capitulo=" + ordenCapituloDelContenidoAnterior.ToString() +
                        "&contenido=" + contenidoAnterior.Orden;

            }
            catch (Exception)
            {

                Session.Add("error", "No se encuentra el contenido solicitado.");
                Response.Redirect("Error.aspx", false);
            }


        }

        public void descargarPdf(byte[] pdf)
        {
            string nombreArchivo = (contenido.Nombre ?? "documento") + "-" + DateTime.Now.ToString("yyyy-MM-dd") + ".pdf";
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
            Response.AddHeader("Content-Length", pdf.Length.ToString());
            Response.ContentType = "text/plain";
            Response.OutputStream.Write(pdf, 0, pdf.Length);
            //Response.End();
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                descargarPdf(contenido.Archivo);
            }
            catch (Exception)
            {
                Session.Add("error", "Error al descargar el archivo.");
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}
