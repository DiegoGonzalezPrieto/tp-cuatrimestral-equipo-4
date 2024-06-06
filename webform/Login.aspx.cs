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

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            UsuarioNegocio userNegocio = new UsuarioNegocio();

            try
            {
                //user = new Usuario(txtEmail.Text, txtPass.Text);

                user.Correo = txtEmail.Text;
                user.Password = txtPass.Text;

                if (userNegocio.Login(user))
                {
                    Session.Add("usuario", user);
                    Response.Redirect("User.aspx", false);
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
