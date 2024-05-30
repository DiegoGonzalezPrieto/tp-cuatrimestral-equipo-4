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
    public partial class AdministracionCategorias : System.Web.UI.Page
    {
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();

        public CategoriaNegocio NegocioCategoria { get; set; } = new CategoriaNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Categorias = NegocioCategoria.listarCategorias();

            if (Categorias.Count == 0)
            {
                // datos de prueba

                Categorias.Add(new Categoria(1, "Marketing"));
                Categorias.Add(new Categoria(2, "Artes"));
                Categorias.Add(new Categoria(3, "Deporte"));
                Categorias.Add(new Categoria(4, "Tecnología"));
            }

            repCategorias.DataSource = Categorias;
            repCategorias.DataBind();

        }
    }
}