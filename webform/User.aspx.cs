using dominio;
using negocio;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace webform
{
    public partial class User : System.Web.UI.Page
    {
        private const string DefaultAvatarUrl = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Seguridad.UsuarioActual != null)
                {
                    Usuario user = (Usuario)Session["usuario"];

                    LblUsername1.Text = user.Username;
                    lblProfesion1.Text = user.Profesion;
                    lblUbicacion1.Text = user.Provincia + ", " + user.Pais;

                    lblVerUsername.Text = user.Username;
                    LblVerNombreApellido.Text = user.Nombre + " " + user.Apellido;
                    lblVerEmail.Text = user.Correo;
                    LblVerProfesion.Text = user.Profesion;
                    LblVerUbicacion.Text = user.Provincia + ", " + user.Pais;

                    if (user.UrlFotoPerfil == string.Empty)
                    {
                        ImgAvatar.ImageUrl = DefaultAvatarUrl;
                        
                    }
                    else
                    {
                        ImgAvatar.ImageUrl = user.UrlFotoPerfil;
                    }

                    btnEditarPerfil.Visible = true;

                }
            }
        }




        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            PanelVerPerfil.Visible = false;
            PanelEditarPerfil.Visible = true;
            btnEditarPerfil.Visible = false;
            btnGuardarPerfil.Visible = true;
            FiCambiarImagen.Visible = true;

            Usuario user = (Usuario)Session["usuario"];

            txtEditarUsername.Text = user.Username;
            txtEditarNombre.Text = user.Nombre;
            txtEditarApellido.Text = user.Apellido;
            txtEditarProfesion.Text = user.Profesion;
            txtEditarProvincia.Text = user.Provincia;
            txtEditarPais.Text = user.Pais;
        }

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario userDP = (Usuario)Session["usuario"];

                userDP.Username = txtEditarUsername.Text;
                userDP.Nombre = txtEditarNombre.Text;
                userDP.Apellido = txtEditarApellido.Text;
                userDP.Profesion = txtEditarProfesion.Text;
                userDP.Provincia = txtEditarProvincia.Text;
                userDP.Pais = txtEditarPais.Text;

                if (FiCambiarImagen.HasFile)
                {
                    var file = FiCambiarImagen.PostedFile;

                    string imagenUrl = SubirImageAImgur(file);
                    if (!string.IsNullOrEmpty(imagenUrl))
                    {
                        ImgAvatar.ImageUrl = imagenUrl;

                        userDP.UrlFotoPerfil = imagenUrl;

                    }
                    else
                    {
                        ///manejo error
                    }
                }

                usuarioNegocio.modificarDatosPersonales(userDP);

                lblVerUsername.Text = userDP.Username;
                LblVerNombreApellido.Text = userDP.Nombre + " " + userDP.Apellido;
                lblVerEmail.Text = userDP.Correo;
                LblVerProfesion.Text = userDP.Profesion;
                LblVerUbicacion.Text = userDP.Provincia + ", " + userDP.Pais;

                LblUsername1.Text = userDP.Username;
                lblProfesion1.Text = userDP.Profesion;
                lblUbicacion1.Text = userDP.Provincia + ", " + userDP.Pais;

                PanelVerPerfil.Visible = true;
                PanelEditarPerfil.Visible = false;
                btnEditarPerfil.Visible = true;
                btnGuardarPerfil.Visible = false;
                FiCambiarImagen.Visible = false;

                lblMensaje.Text = "Perfil actualizado con éxito.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error al guardar el perfil.";
                lblMensaje.ForeColor= System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }

        }




        private string SubirImageAImgur(HttpPostedFile file)
        {
            try
            {
                string clientId = "baadcf8e3b3831d";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", clientId);

                    using (var content = new MultipartFormDataContent())
                    {
                        var streamContent = new StreamContent(file.InputStream);
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        content.Add(streamContent, "image", file.FileName);

                        var response = client.PostAsync("https://api.imgur.com/3/image", content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().Result;

                            int startIndex = responseContent.IndexOf("\"link\":") + "\"link\":".Length + 1;
                            int endIndex = responseContent.IndexOf("\"", startIndex);
                            string imageUrl = responseContent.Substring(startIndex, endIndex - startIndex);
                            return imageUrl;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                return null;
            }
        }

        protected void btnEditarLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx",false);
        }
    }
}