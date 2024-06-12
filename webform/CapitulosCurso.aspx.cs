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
                txtMensajeGuardado.Visible = false;
                listarCapiturlos();

                camposEdicionEnBlanco();
            }

        }
        protected void camposEdicionEnBlanco(bool en=true)
        {
            if (en)
            {
                btnGuardarCambios.Enabled = false;
                txtOrden.Text = string.Empty;
                txtNombreCapitulo.Text = string.Empty;
                chkEstado.Checked = false;

                txtOrden.Enabled = false;
                txtNombreCapitulo.Enabled = false;
                chkEstado.Enabled = false;
            }
            else
            {
                btnGuardarCambios.Enabled = true;
                txtOrden.Enabled = true;
                txtNombreCapitulo.Enabled = true;
                chkEstado.Enabled = true;
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
                listaCapitulos = listaCapitulos.OrderBy(c => c.Orden).ToList();

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
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
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

        protected void Volver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisCursos.aspx", false);
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            camposEdicionEnBlanco(false);
            txtMensajeGuardado.Visible = false;

            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Capitulo capitulo = CapituloNegocio.obtenerCapitulo(id);

            Session["capituloAEditar"] = capitulo;
            txtOrden.Text = capitulo.Orden.ToString();
            txtNombreCapitulo.Text = capitulo.Nombre;
            chkEstado.Checked = capitulo.Liberado;

            Session["OrdenAEditar"] = capitulo.Orden;
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
                Capitulo capitulo = (Capitulo)Session["capituloAEditar"];
            
                short ordenEditar = (short)Session["OrdenAEditar"];

                capitulo.Nombre = txtNombreCapitulo.Text;
                capitulo.Orden = short.Parse(txtOrden.Text);
                capitulo.Liberado = chkEstado.Checked;

                Capitulo capituloId = CapituloNegocio.obtenerCapituloDeCurso((int)Session["idCursoCreadoSeleccionado"], capitulo.Orden);
                CapituloNegocio.modificarCapitulo(capitulo);

                CapituloNegocio.cambiarOrdenCapituloNuevo(capitulo.Orden, capitulo.Id);
                CapituloNegocio.cambiarOrdenCapituloViejo(ordenEditar, capituloId.Id);

            txtMensajeGuardado.Visible = true;

                camposEdicionEnBlanco();

                listarCapiturlos();
        }
    }
}