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
    public partial class CapitulosCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarCapiturlos();
            }
        }
            
        public void listarCapiturlos()
        {
            if (Session["idCursoCreadoSeleccionado"] != null)
            {
                int id = (int)Session["idCursoCreadoSeleccionado"];

                List<Curso> listaCurso = CursoNegocio.listarCursos(false);
                Curso curso = listaCurso.Find(c => c.Id == id);

                List<Capitulo> listaCapitulos = CapituloNegocio.listarCapitulos(id);

                lblTituloCurso.Text = "Curso de " + curso.Nombre;
                
                try
                {
                    if (listaCapitulos.Count != 0)
                    {
                        lblCapitulo.Visible = false;
                        repCapitulos.DataSource = listaCapitulos;
                        repCapitulos.DataBind();
                    }
                    else
                    {
                        lblCapitulo.Visible = true;
                        repCapitulos.DataSource = null;
                        repCapitulos.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected void btnNuevoCapitulo_Click(object sender, EventArgs e)
        {
            int id = (int)Session["idCursoCreadoSeleccionado"];
            string nombreCapitulo = txtNombre.Text;
            int orden = ((CapituloNegocio.obtenerOrdenCapitulo(id).Orden) + 1);
            CapituloNegocio.insertarCapitulo(id, nombreCapitulo, orden);
            listarCapiturlos();
            txtNombre.Text = string.Empty;
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            Session["idCapituloSeleccionado"] = id;

            Response.Redirect("ContenidoCurso.aspx", false);

        }
    }
}