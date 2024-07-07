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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUsuarioSuspendido.Visible = false;
            lblPassIncorrecto.Visible = false;
            lblEmailIncorrecto.Visible = false;
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {

            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }

            Usuario user = new Usuario();
            UsuarioNegocio userNegocio = new UsuarioNegocio();

            try
            {

                user.Correo = txtEmail.Text;
                //user.Password = txtPass.Text;


                user = UsuarioNegocio.obtenerPorCorreo(user.Correo);
                if (user != null)
                {
                    if (user.Password == txtPass.Text)
                    {
                        if (user.Estado)
                        {
                            Session.Add("usuario", user);

                            if (Seguridad.esAdmin())
                                Response.Redirect("PanelAdministracion.aspx", false);
                            else
                                Response.Redirect("Default.aspx", false);
                        }
                        else
                        {
                            lblUsuarioSuspendido.Visible = true;
                        }
                    }
                    else
                    {
                        lblPassIncorrecto.Visible = true;
                    }
                }
                else
                {
                    lblEmailIncorrecto.Visible = true;
                }


            }
            catch (Exception ex)
            {
                Session.Add("error", "datos incorrectos.");
                Response.Redirect("Error.aspx", true);
            }
        }
    }
}
