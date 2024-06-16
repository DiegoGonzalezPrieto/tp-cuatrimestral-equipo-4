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
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();

        private string Categoria;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Categorias = CategoriaNegocio.listarCategorias();
                RepeaterCategorias.DataSource = Categorias;
                RepeaterCategorias.DataBind();

                if (Seguridad.UsuarioActual == null)
                {
                    lblInicio.Text = "Creá tu cuenta gratis y empezá a aprender hoy mismo";
                    BtnCrearCuenta.Text = "Crear cuenta gratis";
                }
                else
                {
                    lblInicio.Text = "Anotate a un curso y empezá a aprender hoy mismo";
                    BtnCrearCuenta.Text = "Ver cursos";
                }

            }

        }


        protected void BtnCrearCuenta_Click(object sender, EventArgs e)
        {
            if (Seguridad.UsuarioActual == null)
            {
                Response.Redirect("Registro.aspx");
            }
            else
            {
                Response.Redirect("Cursos.aspx");
            }

            }

            protected void CardCategoria_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string categoriaId = btn.CommandArgument;
            Response.Redirect("Cursos.aspx?cat=" + categoriaId);
        }
    }
}