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
        private short ultimoOrden;
        public bool errorTipoContenido = false;
        public bool errorVideo = false;
        public bool errorPdf = false;
        public bool guardado = false;
        public string tipoElegido = "";
        public bool errorNombreContenido = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAvisoDeGuardado.Visible = false;
                //btnGuardarContenido.Enabled = false;
                listarTipoContenidos();

                desactivarCampos();

                if ((Contenido)Session["ContenidoAEditar"] != null)
                {
                    lblTituloNuevoContenido.Text = "Modificar Contenido";
                    
                    btnGuardarContenido.Enabled = true;

                    Contenido contenido = (Contenido)Session["ContenidoAEditar"];
                    txtNombreContenido.Text = contenido.Nombre;
                    DDLTipoContenido.SelectedValue = contenido.Tipo.Id.ToString();
                    
                    tipoElegido = contenido.Tipo.Nombre.ToUpper();

                    txtAreaTexto.InnerHtml = contenido.Texto;
                    if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "Video".ToUpper())
                    {
                        txtUrlVideo.Enabled = true;
                        txtUrlVideo.Text = contenido.UrlVideo;
                    }
                    else if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "Texto".ToUpper())
                    {
                        txtUrlVideo.Enabled = false;
                        FileUpload1.Enabled = false;
                    }
                    else
                    {
                        txtUrlVideo.Enabled = false;
                        FileUpload1.Enabled = true;
                    }
                }
            }

        }
        protected void desactivarCampos()
        {
            FileUpload1.Enabled = false;
            txtUrlVideo.Enabled = false;

            txtNombreContenido.Text = string.Empty;
            txtUrlVideo.Text = string.Empty;
            txtAreaTexto.Value = string.Empty;
            DDLTipoContenido.Text = string.Empty;
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

                if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "Video".ToUpper())
                {
                    tipoElegido = "VIDEO";
                    txtUrlVideo.Enabled = true;
                    FileUpload1.Enabled = false;
                }
                else if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "Texto".ToUpper())
                {
                    tipoElegido = "TEXTO";
                    txtAreaTexto.Disabled = false;
                    txtUrlVideo.Enabled = false;
                    FileUpload1.Enabled = false;
                }
                else if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "PDF".ToUpper())
                {
                    tipoElegido = "PDF";
                    FileUpload1.Enabled = true;
                    txtUrlVideo.Enabled = false;
                }
            }
            else
            {
                FileUpload1.Enabled = false;
                txtUrlVideo.Enabled = false;
            }

        }

        protected void btnGuardarContenido_Click(object sender, EventArgs e)
        {
            tipoElegido = DDLTipoContenido.SelectedItem.Text.ToUpper();
            Page.Validate();

            if (!Page.IsValid)
            {
                // falló algún validador
                return;
            }

            if (tipoElegido == "VIDEO" && string.IsNullOrEmpty(txtUrlVideo.Text))
            {
                errorVideo = true;
                return;
            }

            if (tipoElegido == "PDF" && string.IsNullOrEmpty(FileUpload1.FileName))
            {
                errorPdf = true;
                return;
            }

            if (string.IsNullOrEmpty(DDLTipoContenido.SelectedValue))
            {
                errorTipoContenido = true;
                return;
            }

                if (btnGuardarContenido.Text == "Guardar Contenido")
            {
                bool SC = false;
                try
                {
                    Contenido contenido = new Contenido();
                    ContenidoNegocio contenidoNegocio = new ContenidoNegocio();

                    int idCapitulo = (int)Session["idCapituloSeleccionado"];

                    contenido.IdCapitulo = idCapitulo;
                    string nombreContenido = txtNombreContenido.Text;

                    if (nombreContenido.Length <= 0 || nombreContenido.Length > 50)
                    {
                        errorNombreContenido = true;
                        return;
                    }
                    contenido.Nombre = nombreContenido;
                    contenido.Texto = txtAreaTexto.Value;
                    contenido.Tipo = new TipoContenido();
                    if (!string.IsNullOrEmpty(DDLTipoContenido.SelectedValue))
                    {
                        string algo = DDLTipoContenido.SelectedValue.ToString();
                        contenido.Tipo.Id = int.Parse(DDLTipoContenido.Text);
                        if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "Video".ToUpper())
                        {
                            contenido.UrlVideo = txtUrlVideo.Text;
                            SC = true;
                        }
                        else if(DDLTipoContenido.SelectedItem.Text.ToUpper() == "Texto".ToUpper())
                        {
                            SC = true;
                        }
                        else if (DDLTipoContenido.SelectedItem.Text.ToUpper() == "PDF".ToUpper())
                        {
                            if (FileUpload1.HasFile)
                            {
                                contenido.Archivo = FileUpload1.FileBytes;
                                SC = true;
                            }
                        }
                    }
                    else
                    {
                        //Aplicar metodo para traer el id del campo del tipo Texto
                        contenido.Tipo.Id = 2;
                        contenido.Texto = txtAreaTexto.Value;
                    }
                    if ((Contenido)Session["ContenidoAEditar"] != null)
                    {
                        Contenido edContenido = (Contenido)Session["ContenidoAEditar"];
                        contenido.Id = edContenido.Id;
                        contenido.Orden = edContenido.Orden;
                        if(!SC)
                            contenido.Archivo = edContenido.Archivo;
                        contenidoNegocio.modificarContenido(contenido);
                    }
                    else
                    {
                        int orden = ((ContenidoNegocio.obtenerOrdenContenido(idCapitulo).Orden) + 1);
                        contenido.Orden = (short)orden;
                        contenidoNegocio.agregarContenido(contenido);
                    }
                    desactivarCampos();

                    guardado = true;

                    //btnGuardarContenido.Text = "Aceptar";

                    lblAvisoDeGuardado.Visible = true;

                    //btnVolver.Visible = false;
                    
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
            else
            {
                lblAvisoDeGuardado.Visible = false;
                Response.Redirect("ContenidoCurso.aspx", false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContenidoCurso.aspx", false);
        }

        //protected void txtNombreContenido_TextChanged(object sender, EventArgs e)
        //{
        //    btnGuardarContenido.Enabled = true;
        //}
    }
}