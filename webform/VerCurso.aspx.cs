using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        public bool indice { get; set; } = false;
        public decimal procentajeCompletado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            
            // Carga Inicial

            if (!IsPostBack)
            {
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
                
                if (!(Seguridad.adquirioCurso(curso.Id) || Seguridad.creoCurso(curso.Id)))
                {
                    Response.Redirect("DetallesCurso.aspx?id=" + curso.Id.ToString(), false);
                }
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
                indice = true; // si no tenemos capitulo, mostramos el indice del curso
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

                if (indice)
                {
                    iniciarIndice();
                    return;
                }

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

        private void iniciarIndice()
        {
            curso.Indice = CursoNegocio.obtenerIndice(curso.Id);

            if (Session["usuario"] != null)
            {
                // obtener status completado de cada contenido para este usuario (y porcentaje)

                int idUsuario = (Session["usuario"] as Usuario).Id;

                decimal cantCompletados = 0;
                decimal cantTotal = 0;

                for (int i = 0; i < curso.Indice.Capitulos.Count; i++)
                {
                    CapituloIndice capInd = curso.Indice.Capitulos[i];
                    for (int j = 0; j < capInd.Contenidos.Count; j++)
                    {
                        cantTotal++;
                        ContenidoIndice conInd = capInd.Contenidos[j];
                        int completado = ContenidoNegocio.obtenerContenidoCompletado(idUsuario, conInd.Id);
                        conInd.Completado = completado == 1;

                        if (conInd.Completado)
                            cantCompletados++;

                        curso.Indice.Capitulos[i].Contenidos[j] = conInd;

                    }
                }

                procentajeCompletado = cantCompletados / cantTotal * 100;
            }


            repCapitulos.DataSource = curso.Indice.Capitulos;
            repCapitulos.DataBind();
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


        public void cbxCompletado_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                curso = CursoNegocio.obtenerCurso(int.Parse(Request.QueryString["curso"]));

                CheckBox cb = (CheckBox)sender;
                if (cb.Attributes["data-id-contenido"] != null && Session["usuario"] != null)
                {
                    bool completadoCheckBox = cb.Checked;
                    int idUsuario = ((Usuario)Session["usuario"]).Id;
                    int idContenido = int.Parse(cb.Attributes["data-id-contenido"]);
                    ContenidoNegocio.marcarContenidoCompletado(idUsuario, idContenido, completadoCheckBox);

                    decimal cantContenidos = 0;
                    decimal cantCompletados = 0;

                    

                    List<Capitulo> capitulos = CursoNegocio.obtenerCapitulosCurso(curso.Id);

                    foreach (Capitulo capitulo in capitulos)
                    {
                        List<Contenido> contenidos = CapituloNegocio.obtenerContenidosCapitulo(capitulo.Id);
                        cantContenidos += contenidos.Count;

                        foreach (Contenido contenido in contenidos)
                        {
                            int completado = ContenidoNegocio.obtenerContenidoCompletado(idUsuario, contenido.Id);
                            if (completado == 1)
                                cantCompletados++;
                        }


                    }

                    procentajeCompletado = cantCompletados / cantContenidos * 100;
                }

            }
            catch (Exception)
            {

                Session.Add("error", "Error al marcar contenido como completado.");
                Response.Redirect("Error.aspx", false);
            }

        }
    }
}
