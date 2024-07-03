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
    public partial class DetallesCurso : System.Web.UI.Page
    {
        public int IdCurso { get; set; } = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {

                    int idCurso = Convert.ToInt32(Request.QueryString["id"]);
                    IdCurso = idCurso;


                    Curso curso = CursoNegocio.obtenerCurso(idCurso);

                    MostrarDetallesCurso(curso);
                    MostrarResenas(idCurso);
                }
                else
                {
                    Response.Redirect("Cursos.aspx", true);

                    //MostrarDetallesHardcodeados();
                }
            }
        }

        private void MostrarDetallesCurso(Curso curso)
        {

            lblNombreCurso.Text = curso.Nombre;
            lblDescripcionCurso.Text = curso.Descripcion;
            lblFechaPublicacion.Text = curso.FechaPublicacion.ToString("dd/MM/yyyy");
            lblCategoria.Text = curso.NombresCategorias;
            if (curso.Capitulos != null)
            {
                lblCantidadCapitulos.Text = curso.Capitulos.Count.ToString();
            }
            else
            {
                lblCantidadCapitulos.Text = "0";
            }
            lblEstado.Text = curso.Activo ? "Activo" : "Inactivo";
            lblCosto.Text = curso.Costo.ToString("C");

            imgCurso.ImageUrl = curso.ImagenDataUrl ?? "Media/noImage.svg";

        }

        private void MostrarDetallesHardcodeados()
        {

            lblNombreCurso.Text = "HTML";
            lblDescripcionCurso.Text = "HTML es el lenguaje con el que se define el contenido de las páginas web. Básicamente se trata de un conjunto de etiquetas que sirven para definir el texto y otros elementos que compondrán una página web,como imágenes, listas, vídeos, etc. El HTML se creó en un principio con objetivos divulgativos de información con texto y algunas imágenes. No se pensó que llegara a ser utilizado para crear área de ocio y consulta con carácter multimedia (lo que es actualmente la web), de modo que, el HTML se creó sin dar respuesta a todos los posibles usos que se le iba a dar y a todos los colectivos de gente que lo utilizarían en un futuro. Sin embargo, pese a esta deficiente planificación, si que se han ido incorporando modificaciones con el tiempo, estos son los estándares del HTML. Numerosos estándares se han presentado ya. El HTML 4.01 es el último estándar a febrero de 2001.";
            lblFechaPublicacion.Text = "25/05/2024";
            lblCategoria.Text = "Programación Web";
            lblCantidadCapitulos.Text = "10";
            lblEstado.Text = "Activo";
            lblCosto.Text = "$15.000";


            imgCurso.ImageUrl = "Media/software.svg";
        }

        protected void btnInscribirse_Click(object sender, EventArgs e)
        {

            Usuario usuario = (Usuario)Session["usuario"];
            int idUsuario = usuario.Id;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int idCurso = Convert.ToInt32(Request.QueryString["id"]);
                IdCurso = idCurso;
                DateTime fechaAdquisicion = DateTime.Now;
                bool adquisicionConfirmada = true;
                bool estado = true;

                try
                {
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    usuarioNegocio.InscribirCurso(idCurso, idUsuario, fechaAdquisicion, adquisicionConfirmada, estado);

                    CursoNegocio.estadisticaCurso(idCurso);
                    lblMensaje.Text = "Inscripción realizada con éxito.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Visible = true;
                    btnInscribirse.Visible = false;

                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al inscribirse.");
                    Response.Redirect("Error.aspx", false);
                }
            }


        }

        protected void BtnDenunciar_Click(object sender, EventArgs e)
        {
            if (webform.Seguridad.UsuarioActual != null)
            {
                int idCurso = Convert.ToInt32(Request.QueryString["id"]);

                Response.Redirect($"DenunciarCurso.aspx?id={idCurso}");
            }
            else
            {

            }



        }

        private void MostrarResenas(int idCurso)
        {
            List<Resena> resenas = ResenaNegocio.listarResenasDeCurso(idCurso);
            
            rptComments.DataSource = resenas;
            rptComments.DataBind();
        }

        protected void BtnResena_Click(object sender, EventArgs e)
        {
            int idCurso = Convert.ToInt32(Request.QueryString["id"]);

            if (webform.Seguridad.creoCurso(idCurso)) // el usuario CREO el curso
            {
                pnlResena2.Visible = true;
            }
            else if (webform.Seguridad.adquirioCurso(idCurso)) // el usuario COMPRO el curso
            {
                pnlResena1.Visible = true;

            }
            else if (webform.Seguridad.UsuarioActual != null) // el usuario NO COMPRO NI CREO el curso
            {
                pnlResena3.Visible = true;
            }
            else // NO HAY USUARIO
            {
                pnlResena4.Visible = true;
            }

        }

        protected void btnEnviarResena_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                short puntaje;
                if (short.TryParse(txtPuntaje.Text, out puntaje))
                {
                    string mensaje = txtMensaje.Text;

                    if (string.IsNullOrWhiteSpace(mensaje) || !short.TryParse(txtPuntaje.Text, out puntaje) || puntaje < 1 || puntaje > 5)
                    {
                        Session.Add("error", "Debe completar todos los campos correctamente");
                        Response.Redirect("Error.aspx", false);
                    }



                    Usuario user = (Usuario)Session["usuario"];
                    int idCurso = Convert.ToInt32(Request.QueryString["id"]);

                    Resena nuevaResena = new Resena
                    {
                        Usuario = user,
                        IdCurso = idCurso,
                        Puntaje = puntaje,
                        Mensaje = mensaje,
                        FechaCreacion = DateTime.Now,
                        Activa = false
                    };

                    ResenaNegocio resenaNegocio = new ResenaNegocio();
                    resenaNegocio.agregarResena(nuevaResena);

                    //resenaNegocio.listarResenas(idCurso, true);
                    MostrarResenas(idCurso);

                    pnlResena1.Visible = false;

                    txtPuntaje.Text = string.Empty;
                    txtMensaje.Text = string.Empty;
                }

            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx", false);
        }

        protected string obtenerMensajeDenuncia()
        {
            if (Seguridad.UsuarioActual == null)
                return "";

            List<DenunciaCurso> denunciasCurso = DenunciaCursoNegocio.ListarDenuncias();

            string resultado = "";

            resultado = denunciasCurso.Find(d => d.IdDenunciante == Seguridad.UsuarioActual.Id && d.IdCurso == IdCurso).MensajeDenuncia;

            return resultado;
        }
    }
}
