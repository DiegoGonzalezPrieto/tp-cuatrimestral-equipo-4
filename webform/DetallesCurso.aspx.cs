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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    
                    int idCurso = Convert.ToInt32(Request.QueryString["id"]);

                    
                    Curso curso = CursoNegocio.obtenerCurso(idCurso);

                    MostrarDetallesCurso(curso);
                }
                else
                {
                    MostrarDetallesHardcodeados();
                }
            }
        }

        private void MostrarDetallesCurso(Curso curso)
        {
            
            lblNombreCurso.Text = curso.Nombre;
            lblDescripcionCurso.Text = curso.Descripcion;
            lblFechaPublicacion.Text = curso.FechaPublicacion.ToString("dd/MM/yyyy");
            lblCategoria.Text = "cat";
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

            imgCurso.ImageUrl =  curso.ImagenDataUrl ?? "Media/noImage.svg";
            
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
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                int idUsuario = usuario.Id;

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int idCurso = Convert.ToInt32(Request.QueryString["id"]);
                    DateTime fechaAdquisicion = DateTime.Now;
                    bool adquisicionConfirmada = true;
                    bool estado = true;

                    try
                    {
                        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                        usuarioNegocio.InscribirCurso(idCurso, idUsuario, fechaAdquisicion, adquisicionConfirmada, estado);
                        lblMensaje.Text = "Inscripción realizada con éxito.";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", "Error al inscribirse.");
                        Response.Redirect("Error.aspx", false);
                    }
                }
            }
            else
            {
                lblMensaje.Text = "Debe iniciar sesión para inscribirse en un curso.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;

            }
        }

        protected void BtnDenunciar_Click(object sender, EventArgs e)
        {
            int idCurso = Convert.ToInt32(Request.QueryString["id"]);

            Response.Redirect($"DenunciarCurso.aspx?id={idCurso}");

        }
    }
}
