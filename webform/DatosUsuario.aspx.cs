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
    public partial class DatosUsuario : System.Web.UI.Page
    {
        public int IdUsuario { get; set; } = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                    IdUsuario = idUsuario;
                }
                listarCursos();
            }
        }

        public void listarCursos()
        {
            List<Curso> listaCursos = CursoNegocio.listarCursos();

            gvCursosUsuario.DataSource = listaCursos;
            gvCursosUsuario.DataBind();


        }
    }
}