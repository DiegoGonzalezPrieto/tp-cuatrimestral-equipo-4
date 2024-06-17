using dominio;
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

                    lblUsuario2.Text = user.Username;
                    LblNombreApellido2.Text = user.Nombre + " " + user.Apellido;
                    lblEmail2.Text = user.Correo;
                    LblProfesion2.Text = user.Profesion;
                    LblUbicacion2.Text = user.Pais + ", " + user.Provincia;

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



                /* if (ViewState["AvatarUrl"] == null)
                 {
                     ImgAvatar.ImageUrl = DefaultAvatarUrl;
                 }
                 else
                 {
                     ImgAvatar.ImageUrl = ViewState["AvatarUrl"].ToString();
                 }*/
            }
        }

        protected void BtnAvatar_Click(object sender, EventArgs e)
        {
           /* if (FileUpload1.HasFile)
            {
                var file = FileUpload1.PostedFile;

                string imagenUrl = SubirImageAImgur(file);
                if (!string.IsNullOrEmpty(imagenUrl))
                {
                    ImgAvatar.ImageUrl = imagenUrl;
                    ViewState["AvatarUrl"] = imagenUrl;
                }
                else
                {
                    lblMessage.Text = "Error al cargar la imagen. Intente nuevamente con otra imagen";
                }
            }*/
        }



        protected void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            txtUsuario2.Visible = true;
            txtNombreApellido2.Visible = true;
            txtProfesion2.Visible = true; 
            txtUbicacion2.Visible = true;
            btnGuardarPerfil.Visible = true;
            FiCambiarImagen.Visible = true;

            LblUsername1.Visible = false;
            lblProfesion1.Visible = false;
            lblUbicacion1.Visible = false;
            lblUsuario2.Visible = false;
            LblNombreApellido2.Visible = false;
            lblEmail1.Visible = false;
            lblEmail2.Visible = false;
            LblProfesion2.Visible = false;
            LblUbicacion2.Visible = false;
            btnEditarPerfil.Visible = false;

            txtUsuario2.Text = lblUsuario2.Text;
            txtNombreApellido2.Text = LblNombreApellido2.Text;
            txtProfesion2.Text = LblProfesion2.Text;
            txtUbicacion2.Text = LblUbicacion2.Text;
        }

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {

            txtUsuario2.Visible = false;
            txtNombreApellido2.Visible = false;
            txtProfesion2.Visible = false;
            txtUbicacion2.Visible = false;
            btnGuardarPerfil.Visible = false;

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