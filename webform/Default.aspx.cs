using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace webform
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Curso> Cursos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCategorias_Click(object sender, EventArgs e)
        {
            
        }

        protected void BtnMarketing_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=marketing y negocios");
        }

        protected void BtnSoftware_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=informatica y software");
        }

        protected void BtnDesarrolloPersonal_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=desarrollo personal");
        }

        protected void BtnIdiomas_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=idiomas y lenguas");
        }

        protected void BtnArte_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=arte");
        }

        protected void BtnCiencia_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=ciencia y tecnologia");
        }

        protected void BtnCrearCuenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }

        protected void BtnCat1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=categoria extra 1");
        }

        protected void BtnCat2_Click(object sender, EventArgs e)
        {

            Response.Redirect("Cursos.aspx?cat=categoria extra 2");
        }

        protected void BtnCat3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cursos.aspx?cat=categoria extra 3");

        }
    }
}