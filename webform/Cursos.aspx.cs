using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class Cursos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string cat = Request.QueryString["cat"];
                if (!string.IsNullOrEmpty(cat))
                {
                    lblMensaje.Text = $"Actualmente no hay ningún curso de {cat}.";
                }
                else
                {
                    lblMensaje.Text = "No hay ningun curso en este momento...";
                }


            }
        }
    }
}