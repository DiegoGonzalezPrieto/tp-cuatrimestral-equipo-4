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
    public partial class AdministrarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               listarUsuarios();
               listarAdministradores();
               cantidadCursos();
               cantidadUsuarios();
                porcentajeCursoXUsuarios();
            }
        }

        protected void listarUsuarios()
        {
            List<Usuario> listaUsuario = UsuarioNegocio.listarUsuarios();

            try
            {
                gvUsuarios.DataSource = listaUsuario;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void listarAdministradores()
        {
            List<Usuario> listaAdmin = UsuarioNegocio.listarAdministradores();

            try
            {
                gvAdmin.DataSource = listaAdmin;
                gvAdmin.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void cantidadCursos()
        {
            lblCursos.Text = EstadisticaNegocio.CantidadCurso();
        }

        protected void cantidadUsuarios()
        {
            lblUsuarios.Text = EstadisticaNegocio.CantidadUsuarios();
        }

        protected void porcentajeCursoXUsuarios()
        {
            float cursos = float.Parse(lblCursos.Text);
            float usuarios = float.Parse(lblUsuarios.Text);
            float porcentaje = (cursos / usuarios)*100;
            lblPorcentajeCursoxUsuarios.Text = porcentaje.ToString()+ "%";
        }
    }
}