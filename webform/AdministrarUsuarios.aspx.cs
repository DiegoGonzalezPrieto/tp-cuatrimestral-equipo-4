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
    public partial class AdministrarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarUsuarios();
                listaCursos();
                listarAdministradores();
                cantidadCursos();
                cantidadUsuarios();
                porcentajeCursoXUsuarios();
                inscripcionesYcertificaciones();
            }
        }

        protected void listarUsuarios()
        {
            List<Usuario> listaUsuario = UsuarioNegocio.listarUsuarios();

            try
            {
                gvUsuarios.DataSource = listaUsuario;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void listaCursos()
        {
            List<Curso> listaCurso = CursoNegocio.listarCursos();

            try
            {
                gvCursos.DataSource = listaCurso;
                gvCursos.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void listarAdministradores()
        {
            List<Usuario> listaAdmin = UsuarioNegocio.listarAdministradores();

            try
            {
                gvAdmin.DataSource = listaAdmin;
                gvAdmin.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void cantidadCursos()
        {
            lblCursosTotales.Text = EstadisticaNegocio.CantidadCurso();
            lblCursosActivos.Text = EstadisticaNegocio.CantidadCursoActivos();
            int cursosTotales = int.Parse(lblCursosTotales.Text);
            int cursosActivos = int.Parse(lblCursosActivos.Text);
            int cursosEliminados = cursosTotales - cursosActivos;
            lblCursosEliminados.Text = cursosEliminados.ToString();
        }

        protected void cantidadUsuarios()
        {
            lblUsuariosTotales.Text = EstadisticaNegocio.CantidadUsuarios();
            lblUsuariosActivos.Text = EstadisticaNegocio.CantidadUsuariosActivos();
            int usuariosTotales = int.Parse(lblUsuariosTotales.Text);
            int usuariosActivos = int.Parse(lblUsuariosActivos.Text);
            int usuariosSuspendidos = usuariosTotales - usuariosActivos;
            lblUsuariosSuspendidos.Text = usuariosSuspendidos.ToString();
        }

        protected void porcentajeCursoXUsuarios()
        {
            float cursos = float.Parse(lblCursosTotales.Text);
            float usuarios = float.Parse(lblUsuariosTotales.Text);
            float porcentaje = (cursos / usuarios) * 100;
            lblPorcentajeCursoxUsuarios.Text = porcentaje.ToString() + "%";
        }

        protected void inscripcionesYcertificaciones()
        {
            lblInscripciones.Text = EstadisticaNegocio.InscripcionesTotales();
            lblCertificaciones.Text = EstadisticaNegocio.Certificaciones();
        }
    }
}