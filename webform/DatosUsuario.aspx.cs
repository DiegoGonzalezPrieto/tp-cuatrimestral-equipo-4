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

                    List<Usuario> listaUsuario = UsuarioNegocio.listarUsuarios();
                    Usuario usuario = listaUsuario.Find(x => x.Id == IdUsuario);

                    if (usuario.Estado)
                    {
                        btnSuspenderUsuario.Text = "Suspender";
                        btnSuspenderUsuario.CssClass = "btn btn-sm btn-outline-danger";
                    }
                    else
                    {
                        btnSuspenderUsuario.Text = "Activar";
                        btnSuspenderUsuario.CssClass = "btn btn-sm btn-outline-success";     
                    }
                }

                if (Session["tiempoInicial"] == null)
                {
                    Session["tiempoInicial"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                }

                datosUsuario();
                listarCursos();
            }

            if (Session["timpoInicial"] != null)
                tiempoInicialEnSegundos = Session["tiempoInicial"].ToString();

            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];

            if (eventTarget == "tiempoFinalizado" && !string.IsNullOrEmpty(eventArgument))
            {
                int filaId;
                if (int.TryParse(eventArgument, out filaId))
                {
                    tiempoFinalizado(filaId);
                }
            }


        }

        public string tiempoInicialEnSegundos { get; set; }

        private void tiempoFinalizado(int id)
        {
            CursoNegocio.eliminarCurso(id);
            Session.Remove("tiempoInicial");
            listarCursos();
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

            Session["UsuarioS"] = usuario;
            if(usuario != null)
                Session["IdUsuario"] = usuario.Id;
            if (usuario != null)
            {
                if(usuario.UrlFotoPerfil != "")
                {
                    ImgFotoPerfil.ImageUrl = usuario.UrlFotoPerfil;
                }
                else
                {
                    ImgFotoPerfil.ImageUrl = "Media/Usuario.png";
                }  
                
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
            Session["btnSuspenderAction"] = btn.Text.Trim().ToLower();

        }

        protected void btnSuspenderActivar_Click(object sender, EventArgs e)
        {
            if (Session["btnSuspender"] != null)
            {
                int id = (int)Session["btnSuspender"];
                string action = Session["btnSuspenderAction"].ToString();
                Usuario usuario = (Usuario)Session["UsuarioS"];

                List<Curso> listaCurso = CursoNegocio.listarCursos(false, false);
                Curso curso = listaCurso.Find(c => c.Id == id);

                try
                {
                    if (curso.suspencionCurso)
                    {
                        CursoNegocio.activarCurso(id);
                        CursoNegocio.desactivarSuspencionCurso(id);
                        ScriptManager.RegisterStartupScript(this, GetType(), "ReiniciarTemporizador", $"manejarAccion({id}, 'suspender');", true);
                    }
                    else
                    {
                        CursoNegocio.activarSuspencionCurso(id);
                        CursoNegocio.desactivarCurso(id);
                        ScriptManager.RegisterStartupScript(this, GetType(), "IniciarTemporizador", $"manejarAccion({id}, 'activar');", true);

                        EmailService emailService = new EmailService();

                        emailService.avisoDeSuspencionCurso(usuario.Correo, curso.Nombre, usuario.Username);
                        emailService.enviarEmail();
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

        protected void btnSuspenderUsuario_Click(object sender, EventArgs e)
        {
            int id = (int)Session["IdUsuario"];
            List<Usuario> listaUsuario = UsuarioNegocio.listarUsuarios();
            Usuario usuario = listaUsuario.Find(x => x.Id == id);

            Session["EstadoUsuario"] = usuario.Estado;
            if (usuario.Estado)
            {
                
                btnSuspenderUsuario.Text = "Activar";
                btnSuspenderUsuario.CssClass = "btn btn-sm btn-outline-success"; 
                UsuarioNegocio.suspenderUsuario(usuario.Id);
            }
            else
            {
                
                btnSuspenderUsuario.Text = "Suspender";
                btnSuspenderUsuario.CssClass = "btn btn-sm btn-outline-danger"; 
                UsuarioNegocio.activarUsuario(usuario.Id);
            }
        }

    }
}