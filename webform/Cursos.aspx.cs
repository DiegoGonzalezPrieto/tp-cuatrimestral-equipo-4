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
        public Categoria categoriaSeleccionada {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
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

                    if (listaCursos.Count > 0)
                    {
                        repCursos.DataSource = listaCursos;
                        repCursos.DataBind();
                    }
                    else
                    {
                        lblMensaje.Text = "No hay ningún curso disponible en este momento.";
                    }
                }


            }
        }


        protected void BtnCurso_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string idCurso = btn.CommandArgument;
            Response.Redirect($"DetallesCurso.aspx?id={idCurso}");
        }
    }
}