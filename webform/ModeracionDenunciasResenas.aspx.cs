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
    public partial class ModeracionDenunciasResenas : System.Web.UI.Page
    {
        public List<DenunciaResena> denuncias { get; set; } = new List<DenunciaResena>();
        public List<Curso> cursos { get; set; } = new List<Curso>();
        public List<Usuario> usuarios { get; set; } = new List<Usuario>();
        public List<Resena> resenas { get; set; } = new List<Resena>();
        protected void Page_Load(object sender, EventArgs e)
        {
            cursos = CursoNegocio.listarCursos(false, false);
            usuarios = UsuarioNegocio.listarUsuarios();
            resenas = ResenaNegocio.listarResenas();
            denuncias = DenunciaResenaNegocio.ListarDenuncias();

            if (!IsPostBack)
            {
                // da error al revincular el datasource e intentar Eval(Id)
                repDenunciasCursos.DataSource = denuncias;
                repDenunciasCursos.DataBind();

            }

        }

        protected string getNombreCurso(int idResenia)
        {
            int idCurso = getIdCurso(idResenia);
            return cursos.Find(c => c.Id == idCurso).Nombre ?? "...";
        }
        protected int getIdCurso(int idResenia)
        {
            return resenas.Find(r => r.Id == idResenia).IdCurso;
        }
        protected string getNombreUsuario(int idUsuario)
        {
            return usuarios.Find(u => u.Id == idUsuario).Username ?? "...";
        }
        protected string getResena(int idResena)
        {
            return resenas.Find(r => r.Id == idResena).Mensaje;
        }


        protected void btnMarcarResuelto_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idDenuncia = int.Parse(btn.CommandArgument);
            DenunciaResenaNegocio.DenunciaResuelta(idDenuncia);

            denuncias = DenunciaResenaNegocio.ListarDenuncias();
            repDenunciasCursos.DataSource = denuncias;
            repDenunciasCursos.DataBind();
        }

        protected void btnMarcarPendiente_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idDenuncia = int.Parse(btn.CommandArgument);
            DenunciaResenaNegocio.DenunciaPendiente(idDenuncia);

            denuncias = DenunciaResenaNegocio.ListarDenuncias();
            repDenunciasCursos.DataSource = denuncias;
            repDenunciasCursos.DataBind();
        }
    }
}