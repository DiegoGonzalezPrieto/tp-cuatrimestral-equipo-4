using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page is Login || Page is Error || Page is Registro || Page is Default || Page is Cursos || Page is DetallesCurso))
            {
                if (Seguridad.UsuarioAcual == null)
                {
                    Session.Add("mensaje-login", "Debe iniciar sesión para acceder.");
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("usuario");
            Response.Redirect("Default.aspx", false);
        }
    }
}