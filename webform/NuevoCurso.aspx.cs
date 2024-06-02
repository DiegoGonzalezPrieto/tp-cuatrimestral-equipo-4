using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace webform
{
    public partial class NuevoCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<Categoria> categorias = CategoriaNegocio.listarCategorias();
            if (!IsPostBack)
            {
                DDLCategorias1.DataSource = categorias;
                DDLCategorias1.DataTextField = "Nombre";
                DDLCategorias1.DataValueField = "Id";
                DDLCategorias1.DataBind();

                DDLCategorias1.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                DDLCategorias2.DataSource = categorias;
                DDLCategorias2.DataTextField = "Nombre";
                DDLCategorias2.DataValueField = "Id";
                DDLCategorias2.DataBind();

                DDLCategorias2.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                DDLCategorias3.DataSource = categorias;
                DDLCategorias3.DataTextField = "Nombre";
                DDLCategorias3.DataValueField = "Id";
                DDLCategorias3.DataBind();

                DDLCategorias3.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
        }

        protected void btnGuardarNuevoCurso_Click(object sender, EventArgs e)
        {
            try
            {
                Curso nuevoCurso = new Curso();
                CursoNegocio cursoNegocio = new CursoNegocio();

                /* nuevoCurso.IdUsuario = aqui va el Id del usuario creador del curso*/
                nuevoCurso.Nombre = nombreCurso.Text;
                nuevoCurso.Descripcion = descripcionCurso.Text;
                nuevoCurso.Costo = decimal.Parse(costoCurso.Text);
                string etiquetas = etiquetasCurso.Text;
                List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                nuevoCurso.Etiquetas = listaEtiquetas;
                nuevoCurso.UrlImagen = ImagenCategoria.FileBytes;

                cursoNegocio.agregarCurso(nuevoCurso);
                resetearCampos();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }

        private void resetearCampos()
        {
            nombreCurso.Text = string.Empty;
            descripcionCurso.Text= string.Empty;
            costoCurso.Text = string.Empty;
            etiquetasCurso.Text = string.Empty;

        }
    }
}