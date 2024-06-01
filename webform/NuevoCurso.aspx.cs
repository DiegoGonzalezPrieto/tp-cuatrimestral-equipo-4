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
    public partial class NuevoCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardarNuevoCurso_Click(object sender, EventArgs e)
        {
            try
            {
                Curso nuevoCurso = new Curso();
                CursoNegocio cursoNegocio = new CursoNegocio();

                /* nuevoCurso.IdUsuario = */
                nuevoCurso.Nombre = nombreCurso.Text;
                nuevoCurso.Descripcion = descripcionCurso.Text;
                nuevoCurso.Costo = decimal.Parse(costoCurso.Text);
                string etiquetas = etiquetasCurso.Text;
                List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                nuevoCurso.Etiquetas = listaEtiquetas;
                nuevoCurso.UrlImagen = ImagenCategoria.FileBytes;

                cursoNegocio.agregarCurso(nuevoCurso);

                
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }
        }
    }
}