using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace webform
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Curso> Cursos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}