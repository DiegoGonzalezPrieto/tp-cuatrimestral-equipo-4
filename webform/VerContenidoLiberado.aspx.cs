using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class VerContenidoLiberado : System.Web.UI.Page
    {
        public Curso curso { get; set; }
        public Capitulo capitulo { get; set; }
        public Contenido contenido { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                try
                {
                    contenido = ContenidoNegocio.obtenerContenido(int.Parse(Request.QueryString["id"]));
                    capitulo = CapituloNegocio.obtenerCapitulo(contenido.IdCapitulo);
                    curso = CursoNegocio.obtenerCurso(capitulo.IdCurso);

                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al visualizar el contenido liberado.");
                    Response.Redirect("Error.aspx", true);
                }

                if (!contenido.Liberado)
                    Response.Redirect("Cursos.aspx", true);

            }
            else
            {
                Response.Redirect("Cursos.aspx", true);
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