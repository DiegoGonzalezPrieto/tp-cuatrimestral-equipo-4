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
    public partial class InfoGeneralCurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                datosCurso();
                listarUsuariosInscripto();
            }

        }

        protected void datosCurso()
        {
            if (Session["idCursoCreadoSeleccionado"] != null)
            {
                int id = (int)Session["idCursoCreadoSeleccionado"];

                Curso curso = CursoNegocio.obtenerCurso(id);

                lblTituloCurso.Text = "Curso de " + curso.Nombre;

                List<Capitulo> capitulos = CapituloNegocio.listarCapitulos(id);

                lblCantCapitulos.Text = capitulos.Count.ToString();

                int cantContenido = 0;

                for (int i = 0; i < capitulos.Count; i++)
                {
                    Capitulo capitulo = capitulos[i];
                    List<Contenido> contenido = ContenidoNegocio.listaContenido(capitulo.Id);
                    cantContenido += contenido.Count;
                }

                lblCantContenidos.Text = cantContenido.ToString();

                lblCantInscriptos.Text = EstadisticaNegocio.InscriptosPorCurso(id);

                List<Resena> listResenia = ResenaNegocio.listarResenasDeCurso(id);

                lblCantResenias.Text = listResenia.Count.ToString();

                float puntajeResenia = 0;

                if (listResenia.Count > 0)
                    puntajeResenia = (float)ResenaNegocio.puntajeResenasDeCurso(id) / listResenia.Count;

                lblCalificacion.Text = puntajeResenia.ToString();

                lblCantComentarios.Text = ComentarioNegocio.listarComentarios(id).Count.ToString();



            }

        }

        protected void listarUsuariosInscripto()
        {
            if (Session["idCursoCreadoSeleccionado"] != null)
            {
                int id = (int)Session["idCursoCreadoSeleccionado"];
                List<Usuario> listaUsuariosInscriptos = UsuarioNegocio.listarUsuariosInscriptos(id);

                gvUsuariosInscriptos.DataSource = listaUsuariosInscriptos;
                gvUsuariosInscriptos.DataBind();
            }
        }
            protected void btnVolver_Click(object sender, EventArgs e)
            {
                Response.Redirect("MisCursos.aspx", false);
            }
        }
    }