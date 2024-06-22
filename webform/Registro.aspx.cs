using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class Registro : System.Web.UI.Page
    {
        public bool errorNombre { get; set; } = false;
        public bool errorEmail { get; set; } = false;
        public bool errorEmailExistente { get; set; } = false;
        public bool errorPassword { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(nombre))
                errorNombre = true;

            if (string.IsNullOrEmpty(email))
                errorEmail = true;
            else
            {
                try
                {
                    MailAddress m = new MailAddress(email);
                }
                catch (Exception)
                {
                    errorEmail = true;
                }
            }

            List<Usuario> usuarios = UsuarioNegocio.listarUsuarios(true);

            if (usuarios.Exists(u => u.Correo == email))
                errorEmailExistente = true;




            if (string.IsNullOrEmpty(password) || password.Length < 5)
                errorPassword = true;

            if (errorNombre || errorEmail || errorPassword || errorEmailExistente)
                return;


            try
            {
                Usuario user = new Usuario();
                UsuarioNegocio userNegocio = new UsuarioNegocio();
                user.Username = nombre;
                user.Correo = email; 
                user.Password = password;
                user.FechaAlta = DateTime.Now;

                int IdUser = userNegocio.insertarNuevo(user);
                user.Id = IdUser;
                Session.Add("usuario", user);
                Response.Redirect("User.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx"); ;
            }




           // Response.Redirect("Default.aspx");
        }
    }
}