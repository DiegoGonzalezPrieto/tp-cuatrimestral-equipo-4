using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class User : System.Web.UI.Page
    {
        private const string DefaultAvatarUrl = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                if (ViewState["AvatarUrl"] == null)
                {
                    ImgAvatar.ImageUrl = DefaultAvatarUrl;
                }
                else
                {
                    ImgAvatar.ImageUrl = ViewState["AvatarUrl"].ToString();
                }
            }
        }

        protected void BtnAvatar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
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
    }
    }