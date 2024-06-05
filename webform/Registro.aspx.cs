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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = new Usuario();
                UsuarioNegocio userNegocio = new UsuarioNegocio();
                user.Nombre = txtNombre.Text;
                user.Correo = txtEmail.Text; 
                user.Password = txtPassword.Text;

                int IdUser = userNegocio.insertarNuevo(user);

                Session.Add("usuario", user);
                Response.Redirect("User.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }




           // Response.Redirect("Default.aspx");
        }
    }
}