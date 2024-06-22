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
    public partial class Cursos : System.Web.UI.Page
    {
        public Categoria categoriaSeleccionada { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {


            string cat = Request.QueryString["cat"];
            if (!string.IsNullOrEmpty(cat))
            {
                categoriaSeleccionada = CategoriaNegocio.obtenerCategoria(int.Parse(cat));

                lblMensaje.Text = "Categoría: " + categoriaSeleccionada.Nombre;

                List<Curso> listaCursos = CursoNegocio.obenerCursosPorCategoria(categoriaSeleccionada.Id);

                if (listaCursos.Count > 0)
                {
                    repCursos.DataSource = listaCursos;
                    repCursos.DataBind();
                }
                else
                {
                    lblMensaje.Text = $"No hay ningún curso disponible en {categoriaSeleccionada.Nombre}.";
                }
            }
            else
            {
                List<Curso> listaCursos = CursoNegocio.listarCursos(true, false);
                List<Categoria> categorias = CategoriaNegocio.listarCategorias();

                if (listaCursos.Count > 0)
                {
                    if (!IsPostBack)
                    {
                        ddlCategorias.DataSource = categorias;
                        ddlCategorias.DataValueField = "Id";
                        ddlCategorias.DataTextField = "Nombre";
                        ddlCategorias.DataBind();

                        ddlCategorias.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                    }

                    repCursos.DataSource = listaCursos;
                    repCursos.DataBind();
                }
                else
                {
                    lblMensaje.Text = "No hay ningún curso disponible en este momento.";
                }
            }



        }


        protected void BtnCurso_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string idCurso = btn.CommandArgument;
            Response.Redirect($"DetallesCurso.aspx?id={idCurso}");
        }

        protected void btnFiltroCategorias_Click(object sender, EventArgs e)
        {
            List<Curso> listaCursos = CursoNegocio.listarCursos(true, false);

            int idCategoria = int.Parse(ddlCategorias.SelectedValue);

            repCursos.DataSource = listaCursos.Where(c => c.Categorias.Exists(cat => cat.Id == idCategoria));
            repCursos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Curso> listaCursos = CursoNegocio.listarCursos(true, false);

            string textoBusqueda = txtBuscar.Text;

            repCursos.DataSource = listaCursos.Where(c => c.Nombre.Contains(textoBusqueda)
                || c.Descripcion.Contains(textoBusqueda)
                || c.Etiquetas.Exists(et => et.Contains(textoBusqueda)));
            repCursos.DataBind();
        }
    }
}