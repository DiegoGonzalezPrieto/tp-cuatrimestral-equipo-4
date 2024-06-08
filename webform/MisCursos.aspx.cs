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

                btnNuevoCapitul.Disabled = true;
                
                btnNuevoContenido.Enabled = false;
                
                lblCapitulo.Visible = false;
                lblContenido.Visible = false;

            }

        }

        protected void btnNuevoCurso_Click(object sender, EventArgs e)
        {
            Session["CursoAEditar"] = null;
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
            Usuario user = (Usuario)Session["usuario"];
            if (user != null)
            {
                List<Curso> listaCursos = CursoNegocio.listarCursosPorId(user.Id, false);
                repCursos.DataSource = listaCursos;
                repCursos.DataBind();
            }
            else
            {
                repCursos.DataSource = null;
                repCursos.DataBind();
            }

        }

        public void listarCapiturlos ()
        {
            int id = (int)Session["idCursoCreadoSeleccionado"];
            List<Capitulo> capitulos = CapituloNegocio.listarCapitulos(id);

            try
            {
                if (capitulos.Count != 0)
                {
                    
                    lblCapitulo.Visible = false;
                    repCapitulos.DataSource = capitulos;
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

        public void listarContenido()
        {
            int id = (int)Session["idCapituloSeleccionado"];
            List<Contenido> contenido = ContenidoNegocio.listaContenido(id);
            try
            {
                if (contenido.Count != 0)
                {
                    lblContenido.Visible = false;

                    repContenido.DataSource = contenido;
                    repContenido.DataBind();
                }
                else
                {
                    lblContenido.Visible = true;

                    repContenido.DataSource = null;
                    repContenido.DataBind();
                }

                }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnEditarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            Session["CursoAEditar"] = curso;
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnActivarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnActivar"] = id;
        }

        protected void btnDesactivarCurso_Click(Object sender, EventArgs e)
        {
            int id = (int)Session["btnActivar"];

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
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnEliminar"] = id;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)Session["btnEliminar"];

                CursoNegocio cursoAEliminar = new CursoNegocio();
                cursoAEliminar.eliminarCurso(id);

                listarCursosCreados();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            Session["idCursoCreadoSeleccionado"] = id;
            

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);     
            
            listarCapiturlos();

            btnNuevoCapitul.Disabled = false;
            //btnNuevoCapitulo.Enabled = true;

            lblTituloCapitulo.Text = "Curso: " + curso.Nombre;

        }

        protected void btnNuevoCapitulo_Click(object sender, EventArgs e)
        {
            int id = (int)Session["idCursoCreadoSeleccionado"];
            string nombreCapitulo = txtNombre.Text;
            int orden = ((CapituloNegocio.obtenerOrdenCapitulo(id).Orden)+1);
            CapituloNegocio.insertarCapitulo(id, nombreCapitulo, orden);
            listarCapiturlos();
            txtNombre.Text = string.Empty;
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            Session["idCapituloSeleccionado"] = id;


            //List<Contenido> listaContenidos = ContenidoNegocio.listaContenido(id);
            //Contenido contenido = listaContenidos.Find(c => c.Id == id);

            listarContenido();

        }
    }
}