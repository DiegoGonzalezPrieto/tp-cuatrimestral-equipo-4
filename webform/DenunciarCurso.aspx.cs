using dominio;
using negocio;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class DenunciarCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DDLTipoDenuncia.Items.Add(new ListItem("Contenido inapropiado", "0"));
            DDLTipoDenuncia.Items.Add(new ListItem("Información incorrecta", "1"));
            DDLTipoDenuncia.Items.Add(new ListItem("Violación de derechos de autor", "2"));
            DDLTipoDenuncia.Items.Add(new ListItem("Comportamiento inapropiado del instructor", "3"));
            DDLTipoDenuncia.Items.Add(new ListItem("Spam o publicidad", "4"));
            DDLTipoDenuncia.Items.Add(new ListItem("Problemas técnicos", "5"));
            DDLTipoDenuncia.Items.Add(new ListItem("Otro", "6"));

            if (!IsPostBack)
            {
                try
                {
                    Usuario user = (Usuario)Session["usuario"];
                    if (user != null)
                    {
                        int idCurso = Convert.ToInt32(Request.QueryString["id"]);

                        Curso curso = CursoNegocio.obtenerCurso(idCurso);

                        emailUsuario.Text = user.Correo;
                        nombreCurso.Text = curso.Nombre;
                        fechaDenuncia.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Debe seleccionar un curso.");
                    Response.Redirect("Error.aspx", true);
                }
            }
        }

        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {

            try
            {
                DenunciaNegocio denunciaNegocio = new DenunciaNegocio();
                Usuario user = (Usuario)Session["usuario"];
                EmailService emailService = new EmailService();

                int idUsuario = user.Id;
                int idCurso = Convert.ToInt32(Request.QueryString["id"]);
                string motivo = motivoDenuncia.Text;
                string asunto = DDLTipoDenuncia.SelectedItem.Text;

                denunciaNegocio.RegistrarDenuncia(idCurso, idUsuario, motivo);

                emailService.RespuestaDenuncia(user.Correo,asunto);
                emailService.enviarEmail();
                
                lblMessage.Text = "Denuncia enviada correctamente. Aguarde y sera redirigido";
                lblMessage.Visible = true;


                Response.AppendHeader("Refresh", "3;url=Cursos.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al registrar la denuncia.");
                Response.Redirect("Error.aspx", true);
            }
        }
    }
}