using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class NuevoContenido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarTipoContenidos();

                txtAreaTexto.Disabled = true;
                FileUpload1.Enabled = false;
                txtUrlVideo.Enabled = false;
            }

            

        }

        protected void listarTipoContenidos()
        {
            List<TipoContenido> listarTipoContenido = TipoContenidoNegocio.listaTipoContenido();

            try
            {
                DDLTipoContenido.DataSource = listarTipoContenido;
                DDLTipoContenido.DataValueField = "Id";
                DDLTipoContenido.DataTextField = "Nombre";
                DDLTipoContenido.DataBind();

                DDLTipoContenido.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void DDLTipoContenido_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(DDLTipoContenido.SelectedValue))
            {
                int tipoContenido = int.Parse(DDLTipoContenido.SelectedValue);

                if (tipoContenido == 1)
                {
                    txtUrlVideo.Enabled = true;
                    txtAreaTexto.Disabled = true;
                    FileUpload1.Enabled = false;
                }
                else if (tipoContenido == 2)
                {
                    txtAreaTexto.Disabled = false;
                    txtUrlVideo.Enabled = false;
                    FileUpload1.Enabled = false;
                }
                else if (tipoContenido == 3)
                {
                    FileUpload1.Enabled = true;
                    txtAreaTexto.Disabled = true;
                    txtUrlVideo.Enabled = false;
                }
            }
            else
            {
                txtAreaTexto.Disabled = true;
                FileUpload1.Enabled = false;
                txtUrlVideo.Enabled = false;
            }

           
        }

        protected void btnGuardarContenido_Click(object sender, EventArgs e)
        {
            try
            {
                Contenido contenido = new Contenido();
                ContenidoNegocio contenidoNegocio = new ContenidoNegocio();

                int idCapitulo = (int)Session["idCapituloSeleccionado"];

                contenido.IdCapitulo = idCapitulo;
                contenido.Nombre = txtNombreContenido.Text;
                contenido.Tipo = new TipoContenido();
                if (!string.IsNullOrEmpty(DDLTipoContenido.SelectedValue))
                {
                    contenido.Tipo.Id = int.Parse(DDLTipoContenido.Text);
                    if (contenido.Tipo.Id == 1)
                    {
                        contenido.UrlVideo = txtUrlVideo.Text;
                    }
                    else if (contenido.Tipo.Id == 2)
                    {
                        contenido.Texto = txtAreaTexto.Value;

                    }
                    else if (contenido.Tipo.Id == 3)
                    {
                        contenido.Archivo = FileUpload1.FileBytes;

                    }
                }

                int orden = ((ContenidoNegocio.obtenerOrdenContenido(idCapitulo).Orden) + 1);
                contenido.Orden = (short)orden;
                contenidoNegocio.agregarContenido(contenido);
                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}