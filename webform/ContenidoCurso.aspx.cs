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
    public partial class ContenidoCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarContenido();
                Session["ContenidoAEditar"] = null;
            }
        }

        public void listarContenido()
        {
            if (Session["idCapituloSeleccionado"] != null)
            {
                int id = (int)Session["idCursoCreadoSeleccionado"];
                int idCapitulo = (int)Session["idCapituloSeleccionado"];
               
                List<Capitulo> listaCapitulo = CapituloNegocio.listarCapitulos(id);
                Capitulo capitulo = listaCapitulo.Find(c => c.Id == idCapitulo);
                
                List<Contenido> listarContenido = ContenidoNegocio.listaContenido(idCapitulo);
                
                lblTituloContenido.Text = "Capitulo: " + capitulo.Nombre;
                try
                {
                    if (listarContenido.Count != 0)
                    {
                        lblContenido.Visible = false;

                        repContenido.DataSource = listarContenido;
                        repContenido.DataBind();
                    }
                    else
                    {
                        lblContenido.Visible = true;

                        repContenido.DataSource = null;
                        repContenido.DataBind();
                    }

                }
                catch (Exception ex)
                {

                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
        }

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("CapitulosCurso.aspx", false);
        }

        protected void btnNuevoContenido_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoContenido.aspx", false);
        }

        protected void btnEditarContenido_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Contenido contenido = ContenidoNegocio.obtenerContenido(id);

            Session["ContenidoAEditar"] = contenido;

            Response.Redirect("NuevoContenido.aspx", false);


        }

        protected void btnEliminarContenido_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnEliminarC"] = id;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)Session["btnEliminarC"];

                ContenidoNegocio contenidoNegocio = new ContenidoNegocio();
                contenidoNegocio.eliminacionLogicaContenido(id);

                listarContenido();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}