using dominio;
using negocio;
using System;
using System.CodeDom;
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
            if (!Seguridad.esAdmin())
            {
                Session.Add("error", "Acceso denegado.");
                Response.Redirect("Error.aspx", false);
            }

            if (!IsPostBack)
                listarCategorias();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // TODO : validar que el nombre no esté vacío

            if (lblId.Text == "")
            {
                // nueva categoria
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
            else
            {
                // editando categoria

                int id = int.Parse(lblId.Text);
                List<Categoria> listaCategoria = CategoriaNegocio.listarCategorias(false);
                Categoria catEditando = listaCategoria.Find(c => c.Id == id);

                catEditando.Nombre = NombreCategoria.Text;

                if (ImagenCategoria.HasFile)
                    catEditando.Imagen = ImagenCategoria.FileBytes;

                try
                {
                    CategoriaNegocio.modificarCategoria(catEditando);
                    lblId.Text = "";
                    NombreCategoria.Text = "";
                    btnGuardar.Text = "Guardar";
                    listarCategorias();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }

            }

        }

        public void listarCategorias()
        {
            List<Categoria> listaCategoria = CategoriaNegocio.listarCategorias(false);

            repCategorias.DataSource = listaCategoria;
            repCategorias.DataBind();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            CategoriaNegocio.eliminarFisicamenteCategoria(id);

            listarCategorias();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            btnGuardar.Text = "Modificar";
            List<Categoria> listaCategoria = CategoriaNegocio.listarCategorias(false);
            Categoria catEditando = listaCategoria.Find(c => c.Id == id);
            NombreCategoria.Text = catEditando.Nombre;
            lblId.Text = catEditando.Id.ToString();
        }

        protected void btnDesactivar_Click(object sender, EventArgs e)
        {

            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            List<Categoria> listaCategoria = CategoriaNegocio.listarCategorias(false);
            Categoria categoria = listaCategoria.Find(c => c.Id == id);

            try
            {
            if (categoria.Activo)
                CategoriaNegocio.eliminarLogicamenteCategoria(id);
            else
                CategoriaNegocio.activarLogicamenteCategoria(id);


            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

            listarCategorias();
        }
    }
}