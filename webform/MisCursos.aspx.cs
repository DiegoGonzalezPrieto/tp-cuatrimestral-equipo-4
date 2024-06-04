using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class MisCursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarCursosCreados();
                listarCursosInscripto();

                btnNuevoCapitulo.Enabled = false;
                btnNuevoContenido.Enabled = false;

                
            }
            lblCapitulo.Visible = false;

        }

        protected void btnNuevoCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnVerCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerCurso.aspx", false);
        }

        public void listarCursosInscripto()
        {
            List<Curso> listaCursosInscriptos = CursoNegocio.listarCursosInscripto();

            repCardsCurso.DataSource = listaCursosInscriptos;
            repCardsCurso.DataBind();
            
        }
        public void listarCursosCreados() 
        {
            List<Curso> listaCursos = CursoNegocio.listarCursos(false);
            
            repCursos.DataSource = listaCursos;
            repCursos.DataBind();

        }

        public void listarCapiturlos (Curso curso)
        {
            int id = curso.Id;
            List<Capitulo> capitulos = CapituloNegocio.listarCapitulos(id);

            try
            {
                repCapitulos.DataSource = capitulos;
                repCapitulos.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        protected void btnEditarCurso_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnActivarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            try
            {
                if (curso.Disponible)
                    CursoNegocio.desactivarCurso(id);
                else
                    CursoNegocio.activarCurso(id);


            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

            listarCursosCreados();
        }

        protected void btnEliminarCurso_Click(object sender, EventArgs e)
        {

        }

       

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            if (curso != null)
            {
                lblCapitulo.Visible = false;
                listarCapiturlos(curso);
            }
            else
            {
                lblCapitulo.Visible = true;
            }
            

            btnNuevoCapitulo.Enabled = true;
            

        }
    }
}