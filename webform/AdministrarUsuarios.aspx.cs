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
    }
}