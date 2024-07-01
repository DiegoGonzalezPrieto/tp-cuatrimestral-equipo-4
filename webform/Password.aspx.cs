using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class Password : System.Web.UI.Page
    {
        public bool errorPassword { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if(Seguridad.UsuarioActual != null)
                {
                    PanelCambiarPassword.Visible = true;
                    PanelRecuperarPassword.Visible = false;
                } else
                {
                    PanelRecuperarPassword.Visible = true;
                    PanelCambiarPassword.Visible = false;
                }
            }

        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                string pass = txtPassNueva.Text;
                int IdUsuario = Seguridad.UsuarioActual.Id;

                if (string.IsNullOrEmpty(pass) || pass.Length < 5)
                    errorPassword = true;
                if (errorPassword)
                    return;

                usuarioNegocio.CambiarPass(IdUsuario, pass);

                lblCambiar.Text = "Su contraseña fue cambiada con exito";
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (!Page.IsValid)
                return;

            try
            {
                EmailService emailService = new EmailService();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            string email = txtEmailRecupero.Text;

            string pass = usuarioNegocio.BuscarEmail(email);

            if (!string.IsNullOrEmpty(pass))
            {
                    emailService.RecuperoPassword(email,pass);
                    emailService.enviarEmail();

                    lblMensajeRecupero.Text = "Email enviado con exito";
            }
            else
            {
                lblMensajeRecupero.Text = "El email no pertenece a ningun usuario registrado";
            }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}