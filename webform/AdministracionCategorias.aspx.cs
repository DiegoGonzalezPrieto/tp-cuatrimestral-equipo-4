using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class AdministracionCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            listarCategorias();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria nuevaCategoria = new Categoria();
                CategoriaNegocio negocioCategoria = new CategoriaNegocio();

                nuevaCategoria.Nombre = NombreCategoria.Text;
                nuevaCategoria.Imagen = ImagenCategoria.FileBytes;

                negocioCategoria.agregarCategoria(nuevaCategoria);

                NombreCategoria.Text = "";
                listarCategorias();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void listarCategorias()
        {
            List<Categoria> listaCategoria = CategoriaNegocio.listarCategorias();

            repCategorias.DataSource = listaCategoria;
            repCategorias.DataBind();
        }
    }
}