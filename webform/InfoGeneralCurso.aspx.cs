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
    public partial class InfoGeneralCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                datosCurso();
            }

        }

        protected void datosCurso()
        {
            if (Session["idCursoCreadoSeleccionado"] != null)
            {
                int id = (int)Session["idCursoCreadoSeleccionado"];

                Curso curso = CursoNegocio.obtenerCurso(id);

                lblTituloCurso.Text = "Curso de " + curso.Nombre;
            }
            
        }
    }
}