using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class DatosUsuario : System.Web.UI.Page
    {
        public int IdUsuario { get; set; } = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                    IdUsuario = idUsuario;
                }


                datosUsuario();
                listarCursos();
            }


        }

        public void listarCursos()
        {
            if (IdUsuario == 0 && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                IdUsuario = idUsuario;
            }

            List<Curso> listaCursos = CursoNegocio.listarCursosPorIdUsuario(IdUsuario, false, true);

            gvCursosUsuario.DataSource = listaCursos;
            gvCursosUsuario.DataBind();
        }

        protected void datosUsuario()
        {
            List<Usuario> listaUsuario = UsuarioNegocio.listarUsuarios();
            Usuario usuario = listaUsuario.Find(x => x.Id == IdUsuario);
            if (usuario != null)
            {
                ImgFotoPerfil.ImageUrl = usuario.UrlFotoPerfil;
                lblUserName.Text = usuario.Username;
                lblNombreUsuario.Text = "Nombre: " + usuario.Nombre;
                lblApellidoUsuario.Text = "Apellido: " + usuario.Apellido;
                lblFechaNacimiento.Text = "Fecha Nacimiento: " + usuario.FechaNacimiento.ToString();
                lblProfesion.Text = "Profesion: " + usuario.Profesion;
                lblBibliografia.Text = "Biografia: " + usuario.Biografia;
            }

        }

        protected void btnSuspender_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnSuspender"] = id;
        }

        protected void btnSuspenderActivar_Click(object sender, EventArgs e)
        {
            if (Session["btnSuspender"] != null)
            {
                int id = (int)Session["btnSuspender"];

                List<Curso> listaCurso = CursoNegocio.listarCursos(false, false);
                Curso curso = listaCurso.Find(c => c.Id == id);

                try
                {
                    if (curso.suspencionCurso)
                    {
                        CursoNegocio.activarCurso(id);
                        CursoNegocio.desactivarSuspencionCurso(id);
                    }
                    else
                    {
                        CursoNegocio.activarSuspencionCurso(id);
                        CursoNegocio.desactivarCurso(id);
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
                listarCursos();
            }

        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministrarUsuarios.aspx", false);
        }
    }
}