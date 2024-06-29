using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace webform
{
    public partial class VerCursoLiberado : System.Web.UI.Page
    {
        public Curso curso { get; set; }
        public List<Capitulo> capitulos { get; set; }
        public List<Contenido> contenidosLiberados { get; set; } = new List<Contenido>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                try
                {
                    curso = CursoNegocio.obtenerCurso(int.Parse(Request.QueryString["id"]));
                    capitulos = CapituloNegocio.listarCapitulos(curso.Id);
                    foreach (Capitulo cap in capitulos)
                    {
                        List<Contenido> contenidosLiberadosDelCap = cap.Contenidos.Where(cont => cont.Liberado).ToList();

                        contenidosLiberados.AddRange(contenidosLiberadosDelCap);
                        cap.Contenidos = contenidosLiberadosDelCap.OrderBy(cont => cont.Orden).ToList();
                    }

                    
                    List<int> idsCapitulosConContLiberado = contenidosLiberados.Select(cont => cont.IdCapitulo).ToList();
                    List<Capitulo> capLiberados = capitulos.Where(cap => idsCapitulosConContLiberado.Contains(cap.Id)).ToList();
                    repCapitulos.DataSource = capLiberados.OrderBy(cap => cap.Orden).ToList();
                    repCapitulos.DataBind();

                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error al visualizar contenidos liberados del curso.");
                    Response.Redirect("Error.aspx", true);
                }

                if (contenidosLiberados.Count == 0)
                    Response.Redirect("Cursos.aspx", true);

            }
            else
            {
                Response.Redirect("Cursos.aspx", true);
            }
        }
    }
}