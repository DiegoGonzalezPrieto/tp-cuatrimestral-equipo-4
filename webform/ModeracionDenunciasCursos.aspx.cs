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
        public List<DenunciaCurso> denuncias { get; set; } = new List<DenunciaCurso>();
        public List<Curso> cursos { get; set; } = new List<Curso>();
        public List<Usuario> usuarios { get; set; } = new List<Usuario>();
        protected void Page_Load(object sender, EventArgs e)
        {
            cursos = CursoNegocio.listarCursos(false, false);
            usuarios = UsuarioNegocio.listarUsuarios();
            denuncias = DenunciaCursoNegocio.ListarDenuncias();

            if (!IsPostBack)
            {
                // da error al revincular el datasource e intentar Eval(Id)
                repDenunciasCursos.DataSource = denuncias;
                repDenunciasCursos.DataBind();

            }



        }

        protected string getNombreCurso(int idCurso)
        {
            Curso curso = cursos.Find(c => c.Id == idCurso);
            string nombreCurso = curso != null ? curso.Nombre : "[Nombre de curso]";
            return nombreCurso;
        }
        protected string getNombreUsuario(int idUsuario)
        {
            return usuarios.Find(u => u.Id == idUsuario).Username ?? "...";
        }

        protected void btnMarcarResuelto_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idDenuncia = int.Parse(btn.CommandArgument);
            DenunciaCursoNegocio.DenunciaResuelta(idDenuncia);

            denuncias = DenunciaCursoNegocio.ListarDenuncias();
            repDenunciasCursos.DataSource = denuncias;
            repDenunciasCursos.DataBind();
        }

        protected void btnMarcarPendiente_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idDenuncia = int.Parse(btn.CommandArgument);
            DenunciaCursoNegocio.DenunciaPendiente(idDenuncia);
 
            denuncias = DenunciaCursoNegocio.ListarDenuncias();
            repDenunciasCursos.DataSource = denuncias;
            repDenunciasCursos.DataBind();
        }
    }
}