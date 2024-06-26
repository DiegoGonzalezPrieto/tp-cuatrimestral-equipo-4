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
        protected string activeTab = "usuarios";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ActiveTab"] != null)
                {
                    activeTab = Session["ActiveTab"].ToString();
                }

                Session.Remove("ActiveTab");

                listarUsuarios();
                listaCursos();
                listarAdministradores();
                cantidadCursos();
                cantidadUsuarios();
                porcentajeCursoXUsuarios();
                inscripcionesYcertificaciones();

                DataBind();
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

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

            List<Curso> listaCursoE = CursoNegocio.listarCursosEliminados();

            try
            {
                gvCursosEliminados.DataSource = listaCursoE;
                gvCursosEliminados.DataBind();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
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
            int inscripcionesTotales = EstadisticaNegocio.InscripcionesTotales();
            if (inscripcionesTotales > 0)
            {
                lblInscripciones.Text = inscripcionesTotales.ToString();
            }
            else
            {
                lblInscripciones.Text = "0";
            }
            lblCertificaciones.Text = EstadisticaNegocio.Certificaciones();
        }



        protected void btnVerDatos_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Response.Redirect($"DatosUsuario.aspx?id={id}");
        }

        protected void chkAviso_CheckedChanged(object sender, EventArgs e)
        {
            Session["ActiveTab"] = "cursos";
            CheckBox check = (sender as CheckBox);
            Response.Redirect(Request.RawUrl);
        }

        protected void btnEliminacionTotal_Click(object sender, EventArgs e)
        {

        }
    }
}