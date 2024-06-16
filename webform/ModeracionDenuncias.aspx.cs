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
    public partial class ModeracionDenuncias : System.Web.UI.Page
    {

        public  int denunciasCursosResueltas { get; set; }
        public  int denunciasCursosPendientes { get; set; }
        public  int denunciasReseniasPendientes { get; set; }
        public  int denunciasReseniasResueltas { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            List<DenunciaCurso> denunciasCurso = DenunciaCursoNegocio.ListarDenuncias();

            denunciasCursosResueltas = denunciasCurso.Count(c => c.Resuelta);
            denunciasCursosPendientes = denunciasCurso.Count(c => !c.Resuelta);
           
            List<DenunciaResena> denunciasResena = DenunciaResenaNegocio.ListarDenuncias();

            denunciasReseniasPendientes = denunciasResena.Count(c => c.Resuelta);
            denunciasReseniasPendientes = denunciasResena.Count(c => !c.Resuelta);



        }
    }
}