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
        public decimal porcentajeCompletado { get; set; }
        public int mesesRestantes { get; set; }
        public int diasRestantes { get; set; }

        public List<Comentario> listaComentarios { get; set; } = new List<Comentario>();
        public List<Comentario> listaRespuestas { get; set; } = new List<Comentario>();
        public List<Usuario> usuarios { get; set; } = new List<Usuario>();

        

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int idCurso = int.Parse(Request.QueryString["curso"]);

                    listaComentarios = ComentarioNegocio.listarComentarios(idCurso);
                    usuarios = UsuarioNegocio.listarUsuarios();

                }
                catch (Exception)
                {
                    Session.Add("error", "Seleccione un curso o inicie sesion.");
                    Response.Redirect("Error.aspx", true);
                }
                DataBind();

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            // Carga Inicial
            if (!IsPostBack)
            {

                int idCurso = int.Parse(Request.QueryString["curso"]);
                curso = CursoNegocio.obtenerCurso(idCurso);
                Usuario user = (Usuario)Session["usuario"];
                int idUsuario = user.Id;

                DateTime? FechaAdquisicion = UsuarioNegocio.ObtenerFechaAdquisicion(idUsuario, idCurso);

                if (FechaAdquisicion.HasValue)
                {
                    DateTime FechaFinalizacion = FechaAdquisicion.Value.AddMonths(curso.Duracion);
                    // Linea de prueba:
                    //DateTime FechaFinalizacion = DateTime.Now.AddMinutes(1);
                    DateTime fechaActual = DateTime.Now;

                    TimeSpan diferencia = FechaFinalizacion - fechaActual;

                    if(diferencia > TimeSpan.Zero)
                    {
                        lblTiempoRestante.Text = $"Su suscripción al curso se acabará en {diferencia.Days} días";

                        lblTiempoRestante.Attributes["data-diff"] = diferencia.TotalSeconds.ToString();

                        obtenerIdsContenido();
                        obtenerDatos();

                        rptComentarios.DataSource = listaComentarios;
                        rptComentarios.DataBind();
                    }
                    else
                    {
                        UsuarioNegocio.BajaUsuarioCurso(idUsuario, idCurso);

                        lblMensaje.Text = "El período de su suscripción ha expirado. Espere y será redirigido";
                        lblMensaje.Visible = true;
                        lblTiempoRestante.Visible = false;
                        Response.AppendHeader("Refresh", "3;url=MisCursos.aspx");
                    }
                }
                else
                {
                    lblTiempoRestante.Text = "No se pudo obtener la fecha de adquisición.";

                    obtenerIdsContenido();
                    obtenerDatos();

                    rptComentarios.DataSource = listaComentarios;
                    rptComentarios.DataBind();

                }

                /*
                obtenerIdsContenido();
                obtenerDatos();

                rptComentarios.DataSource = listaComentarios;
                rptComentarios.DataBind();
                */

            }

        }

        private void obtenerIdsContenido()
        {
            try
            {
                int idCurso = int.Parse(Request.QueryString["curso"]);
                curso.Id = idCurso;

                if (!(Seguridad.adquirioCurso(curso.Id) || Seguridad.creoCurso(curso.Id)) && !Seguridad.esAdmin())
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

            if (Session["usuario"] != null && !Seguridad.esAdmin())
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
                if (cantTotal > 0)
                    porcentajeCompletado = cantCompletados / cantTotal * 100;
                else
                    porcentajeCompletado = 100;
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
                obtenerIdsContenido();
                obtenerDatos();

                rptComentarios.DataSource = listaComentarios;
                rptComentarios.DataBind();
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

                    porcentajeCompletado = cantCompletados / cantContenidos * 100;
                }

            }
            catch (Exception)
            {

                Session.Add("error", "Error al marcar contenido como completado.");
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void rptComentarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var comentario = (Comentario)e.Item.DataItem;
                var rptRespuestas = (Repeater)e.Item.FindControl("rptRespuestas");

                if (rptRespuestas != null)
                {
                    var listaRespuestas = ComentarioNegocio.listarRespuestas(comentario.IdCurso, comentario.Id);
                    rptRespuestas.DataSource = listaRespuestas;
                    rptRespuestas.DataBind();
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            string mensaje = txtComentario.Text;

            if (string.IsNullOrWhiteSpace(mensaje))
            {
                Session.Add("error", "No puede enviar mensajes en blanco");
                Response.Redirect("../Error.aspx", false);
            }

            Usuario user = (Usuario)Session["usuario"];
            int idCurso = Convert.ToInt32(Request.QueryString["curso"]);

            Comentario nuevoComentario = new Comentario
            {
                IdUsuario = user.Id,
                IdCurso = idCurso,
                Mensaje = mensaje,
                FechaCreacion = DateTime.Now,
                Activo = false,
                Id_aResponder = -1
            };

            ComentarioNegocio comentarioNegocio = new ComentarioNegocio();
            comentarioNegocio.agregarComentario(nuevoComentario);

            obtenerIdsContenido();
            obtenerDatos();

            listaComentarios = ComentarioNegocio.listarComentarios(idCurso);
            rptComentarios.DataSource = listaComentarios;
            rptComentarios.DataBind();

            txtComentario.Text = string.Empty;

        }

        protected void btnResponderEnviar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string comentarioActualId = btn.CommandArgument;
            int Id_comentarioActual = int.Parse(comentarioActualId);


            RepeaterItem item = (RepeaterItem)btn.NamingContainer;

            TextBox txtResponder = (TextBox)item.FindControl("txtResponder");
            string mensaje = txtResponder.Text;


            if (string.IsNullOrWhiteSpace(mensaje))
            {
                Session.Add("error", "No puede enviar mensajes en blanco");
                Response.Redirect("../Error.aspx", false);
            }

            Usuario user = (Usuario)Session["usuario"];
            int idCurso = Convert.ToInt32(Request.QueryString["curso"]);

            Comentario nuevaRespuesta = new Comentario
            {
                IdUsuario = user.Id,
                IdCurso = idCurso,
                Mensaje = mensaje,
                FechaCreacion = DateTime.Now,
                Activo = false,
                Id_aResponder = Id_comentarioActual
            };

            ComentarioNegocio RespuestaNegocio = new ComentarioNegocio();
            RespuestaNegocio.agregarComentario(nuevaRespuesta);

            obtenerIdsContenido();
            obtenerDatos();

            listaComentarios = ComentarioNegocio.listarComentarios(idCurso);
            rptComentarios.DataSource = listaComentarios;
            rptComentarios.DataBind();

            txtResponder.Text = string.Empty;
        }

        protected string obtenerImagenusuario(int idUsuario)
        {
            if (usuarios.Count == 0)
                usuarios = UsuarioNegocio.listarUsuarios();

            string urlFoto = usuarios.Find(u => u.Id == idUsuario).UrlFotoPerfil;

            return !string.IsNullOrEmpty(urlFoto) ? urlFoto : "Media/Usuario.png";
        }
    }
}
