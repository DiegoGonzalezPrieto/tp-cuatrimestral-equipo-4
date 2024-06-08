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

                int idUsuario = user.Id;
                int idCurso = Convert.ToInt32(Request.QueryString["id"]);
                string motivo = motivoDenuncia.Text;

                denunciaNegocio.RegistrarDenuncia(idCurso, idUsuario, motivo);

               Response.Redirect("Cursos.aspx",false);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al registrar la denuncia.");
                Response.Redirect("Error.aspx", true);
            }
        }
    }
}
