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
    public partial class DenunciarResena : System.Web.UI.Page
    {
        public bool mostrarDenuncia {get; set;} = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Seguridad.UsuarioActual != null)
                    {
                        int idResenia = Convert.ToInt32(Request.QueryString["id"]);

                        Resena r = ResenaNegocio.obtenerResena(idResenia);

                        if (r.Id == 0)
                            throw new Exception();

                        DenunciaResena dr = DenunciaResenaNegocio.ListarDenuncias().Find(d => d.IdDenunciante == Seguridad.UsuarioActual.Id && d.IdReseña == r.Id);

                        if (dr != null && dr.Id != 0)
                        {
                            mostrarDenuncia = true;
                            lblDenunciaRealizada.Text = dr.MensajeDenuncia;
                            fechaDenunciaRealizada.InnerText = dr.FechaCreacion.ToString("dd/MM/yyyy");
                        }



                        Curso curso = CursoNegocio.obtenerCurso(r.IdCurso);

                        emailUsuario.Text = Seguridad.UsuarioActual.Correo;
                        nombreCurso.Text = curso.Nombre;
                        txtResena.Text = r.Mensaje;
                        fechaDenuncia.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Debe seleccionar una reseña a denunciar.");
                    Response.Redirect("Error.aspx", true);
                }
            }
        }
        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {

            Page.Validate();

            if (!Page.IsValid)
                return;

            try
            {
                btnEnviarDenuncia.Enabled = false;

                Usuario user = (Usuario)Session["usuario"];
                EmailService emailService = new EmailService();

                int idUsuario = user.Id;
                int idResenia = Convert.ToInt32(Request.QueryString["id"]);
                string motivo = motivoDenuncia.Text;
                Resena r = ResenaNegocio.obtenerResena(idResenia);

                DenunciaResenaNegocio.RegistrarDenuncia(idResenia, idUsuario, motivo);

                Curso curso = CursoNegocio.obtenerCurso(r.IdCurso);
                emailService.RespuestaDenuncia(user.Correo, $"Denuncia de reseña en curso {curso.Nombre}");
                emailService.enviarEmail();

                lblMessage.Text = "Denuncia enviada correctamente. Aguarde y sera redirigido";
                lblMessage.Visible = true;
                btnEnviarDenuncia.Visible = false;


                Response.AppendHeader("Refresh", "3;url=Cursos.aspx");
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al registrar la denuncia.");
                Response.Redirect("Error.aspx", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            int idResenia = Convert.ToInt32(Request.QueryString["id"]);

            Resena r = ResenaNegocio.obtenerResena(idResenia);
            Curso c = CursoNegocio.obtenerCurso(r.IdCurso);
            Response.Redirect("DetallesCurso.aspx?id=" + c.Id);
        }
    }

}