using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class MisCursos : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAvisoImportante.Visible = false;
            if (!IsPostBack)
            {
                Session["CursoAEditar"] = null;
                listarCursosCreados();
                listarCursosInscripto();
                
            }

        }

        protected void btnNuevoCurso_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnVerCurso_Click(object sender, EventArgs e)
        {
            int idCurso = int.Parse(((Button)sender).CommandArgument);
            Response.Redirect("VerCurso.aspx?curso=" + idCurso, false);
        }

        public void listarCursosInscripto()
        {
            int idUsuario = Seguridad.UsuarioActual != null ? Seguridad.UsuarioActual.Id : 0;
            List<Curso> listaCursosInscriptos = CursoNegocio.listarCursosInscripto(idUsuario);

            repCardsCurso.DataSource = listaCursosInscriptos;
            repCardsCurso.DataBind();
            
        }
        public void listarCursosCreados() 
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user != null)
            {
                List<Curso> listaCursos = CursoNegocio.listarCursosPorIdUsuario(user.Id, false, true);
                
                repCursos.DataSource = listaCursos;
                repCursos.DataBind();

                StringBuilder mensaje = new StringBuilder();

                foreach (Curso c in listaCursos)
                    if (c.suspencionCurso)
                    {
                        DeshabilitarBotonActivarCurso(c.Id);
                        lblAvisoImportante.Visible = true;
                        mensaje.Append($"El curso de {c.Nombre} ha sido suspendido debido a una denuncia, se le envió un mail con más detalles.<br/>");
                    }

                if (mensaje.Length > 0)
                {
                    lblAvisoImportante.Text = mensaje.ToString();
                }
                else
                {
                    lblAvisoImportante.Visible = false;
                }

            }
            else
            {
                repCursos.DataSource = null;
                repCursos.DataBind();
            }

        }

        protected void DeshabilitarBotonActivarCurso(int cursoId)
        {
            foreach (RepeaterItem item in repCursos.Items)
            {
                Button btnActivarCurso = item.FindControl("btnActivarCurso") as Button;
                HiddenField idCurso = item.FindControl("IdCurso") as HiddenField;

                if (btnActivarCurso != null && idCurso != null)
                {
                    if (int.TryParse(idCurso.Value, out int id) && id == cursoId)
                    {
                        btnActivarCurso.Enabled = false;
                        UpdatePanel updatePanel = item.FindControl("UpdatePanel1") as UpdatePanel;
                        updatePanel.Update();
                        break;
                    }
                }
            }
        }

        protected void suspencionCurso()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnEditarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            Session["CursoAEditar"] = curso;
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnActivarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnActivar"] = id;
        }

        protected void btnDesactivarCurso_Click(Object sender, EventArgs e)
        {
            int id = (int)Session["btnActivar"];

            List<Curso> listaCurso = CursoNegocio.listarCursos(false, false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            try
            {
                if (curso.Disponible)
                    CursoNegocio.desactivarCurso(id);
                else
                    CursoNegocio.activarCurso(id);


            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

            listarCursosCreados();
        }

        protected void btnEliminarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnEliminar"] = id;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)Session["btnEliminar"];

                CursoNegocio.eliminarCurso(id);

                listarCursosCreados();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            Session["idCursoCreadoSeleccionado"] = id;
            
            Response.Redirect("CapitulosCurso.aspx", false);

        }

        protected void btnVerInfoCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            Session["idCursoCreadoSeleccionado"] = id;

            Response.Redirect("InfoGeneralCurso.aspx", false);

        }
    }
}