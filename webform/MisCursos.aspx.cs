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
    public partial class MisCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                listarCursosCreados();
        }

        protected void btnNuevoCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnVerCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerCurso.aspx", false);
        }

        public void listarCursosCreados() 
        {
            List<Curso> listaCursos = CursoNegocio.listarCursos();

            repCursos.DataSource = listaCursos;
            repCursos.DataBind();
        }
    }
}