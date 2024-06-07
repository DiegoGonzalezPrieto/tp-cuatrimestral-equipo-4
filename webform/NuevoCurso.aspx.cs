using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace webform
{
    public partial class NuevoCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tituloNuevoCurso.Text = "Nuevo Curso";

            List<Categoria> categorias = CategoriaNegocio.listarCategorias();

            if (!IsPostBack)
            {
                DDLCategorias1.DataSource = categorias;
                DDLCategorias1.DataTextField = "Nombre";
                DDLCategorias1.DataValueField = "Id";
                DDLCategorias1.DataBind();

                DDLCategorias1.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                DDLCategorias2.DataSource = categorias;
                DDLCategorias2.DataTextField = "Nombre";
                DDLCategorias2.DataValueField = "Id";
                DDLCategorias2.DataBind();

                DDLCategorias2.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                DDLCategorias3.DataSource = categorias;
                DDLCategorias3.DataTextField = "Nombre";
                DDLCategorias3.DataValueField = "Id";
                DDLCategorias3.DataBind();

                DDLCategorias3.Items.Insert(0, new ListItem(String.Empty, String.Empty));


                if (Session["CursoAEditar"] != null)
                {
                    try
                    {
                        tituloNuevoCurso.Text = "Modificar Curso";
                        Curso curso = (Curso)Session["CursoAEditar"];
                        Session["IdCursoEditar"] = curso.Id;

                        nombreCurso.Text = curso.Nombre;
                        descripcionCurso.Text = curso.Descripcion;
                        costoCurso.Text = curso.Costo.ToString();
                        if (curso.Etiquetas != null && curso.Etiquetas.Count > 0)
                        {
                            etiquetasCurso.Text = String.Join(", ", curso.Etiquetas);
                        }
                        //ImagenCurso.AccessKey = curso.ImagenDataUrl;
                        chkHabilitarComentario.Checked = curso.ComentariosHabilitados;
                        chkDisponible.Checked = curso.Disponible;
                        if (curso.Categorias != null && curso.Categorias.Count > 0)
                        {
                            if (curso.Categorias.Count > 0)
                                DDLCategorias1.SelectedValue = curso.Categorias.ElementAtOrDefault(0)?.Id.ToString();
                            if (curso.Categorias.Count > 1)
                                DDLCategorias2.SelectedValue = curso.Categorias.ElementAtOrDefault(1)?.Id.ToString();
                            if (curso.Categorias.Count > 2)
                                DDLCategorias3.SelectedValue = curso.Categorias.ElementAtOrDefault(2)?.Id.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Session.Add("error", ex.ToString());
                    }
                }
            }

            
        }

        protected void btnGuardarNuevoCurso_Click(object sender, EventArgs e)
        {
            if (Session["CursoAEditar"] == null)
            {
                try
                {
                    Curso nuevoCurso = new Curso();
                    CursoNegocio cursoNegocio = new CursoNegocio();

                    Usuario user = (Usuario)Session["usuario"];

                    nuevoCurso.IdUsuario = user.Id;
                    nuevoCurso.Nombre = nombreCurso.Text;
                    nuevoCurso.Descripcion = descripcionCurso.Text;
                    nuevoCurso.Costo = decimal.Parse(costoCurso.Text);
                    string etiquetas = etiquetasCurso.Text;
                    List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                    nuevoCurso.Etiquetas = listaEtiquetas;
                    nuevoCurso.UrlImagen = ImagenCurso.FileBytes;
                    nuevoCurso.ComentariosHabilitados = chkHabilitarComentario.Checked;
                    nuevoCurso.Disponible = chkDisponible.Checked;

                    List<int> idsCategorias = new List<int>();

                    if (!string.IsNullOrEmpty(DDLCategorias1.SelectedValue))
                        idsCategorias.Add(int.Parse(DDLCategorias1.SelectedValue));

                    if (!string.IsNullOrEmpty(DDLCategorias2.SelectedValue))
                        idsCategorias.Add(int.Parse(DDLCategorias2.SelectedValue));

                    if (!string.IsNullOrEmpty(DDLCategorias3.SelectedValue))
                        idsCategorias.Add(int.Parse(DDLCategorias3.SelectedValue));
         
                    cursoNegocio.agregarCurso(nuevoCurso, idsCategorias);

                    resetearCampos();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }
            else
            {
                try
                {
                    Curso curso = (Curso)Session["CursoAEditar"];
                    CursoNegocio cursoNegocio = new CursoNegocio();

                    curso.Nombre = nombreCurso.Text;
                    curso.Descripcion = descripcionCurso.Text;
                    curso.Costo = decimal.Parse(costoCurso.Text);
                    string etiquetas = etiquetasCurso.Text;
                    List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                    curso.Etiquetas = listaEtiquetas;
                    if (ImagenCurso.HasFile)
                        curso.UrlImagen = ImagenCurso.FileBytes;
                    curso.ComentariosHabilitados = chkHabilitarComentario.Checked;
                    curso.Disponible = chkDisponible.Checked;

                    List<int> idsCategorias = new List<int>();

                    if (!string.IsNullOrEmpty(DDLCategorias1.SelectedValue))
                        idsCategorias.Add(int.Parse(DDLCategorias1.SelectedValue));

                    if (!string.IsNullOrEmpty(DDLCategorias2.SelectedValue))
                        idsCategorias.Add(int.Parse(DDLCategorias2.SelectedValue));

                    if (!string.IsNullOrEmpty(DDLCategorias3.SelectedValue))
                        idsCategorias.Add(int.Parse(DDLCategorias3.SelectedValue));
   
                    cursoNegocio.modificarCurso(curso, idsCategorias);

                    resetearCampos();
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                }
            }  
        }

        private void resetearCampos()
        {
            lblNombreCursoAgregado.InnerText = nombreCurso.Text;
            nombreCurso.Text = string.Empty;
            descripcionCurso.Text= string.Empty;
            costoCurso.Text = string.Empty;
            etiquetasCurso.Text = string.Empty;
            DDLCategorias1.SelectedValue = string.Empty;
            DDLCategorias2.SelectedValue = string.Empty;
            DDLCategorias3.SelectedValue = string.Empty;

        }

        protected void btnModalAceptar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisCursos.aspx", false);
        }
    }
}