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
                user.Password = txtPass.Text;

                if (userNegocio.Login(user))
                {
                    user = UsuarioNegocio.obtenerPorCorreo(user.Correo);
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
                    Session.Add("error", "datos incorrectos");
                    Response.Redirect("../Error.aspx", false);
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
