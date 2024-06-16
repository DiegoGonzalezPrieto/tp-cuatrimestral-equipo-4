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
    public partial class ModeracionDenunciasCursos : System.Web.UI.Page
    {
        public List<DenunciaCurso> denuncias {  get; set; } = new List<DenunciaCurso> ();
        protected void Page_Load(object sender, EventArgs e)
        {
            denuncias = DenunciaCursoNegocio.ListarDenuncias();
            repDenunciasCursos.DataSource = denuncias;
            repDenunciasCursos.DataBind();

        }
    }
}