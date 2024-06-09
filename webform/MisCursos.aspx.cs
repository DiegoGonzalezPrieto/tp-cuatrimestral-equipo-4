﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webform
{
    public partial class MisCursos : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listarCursosCreados();
                listarCursosInscripto();
            }

        }

        protected void btnNuevoCurso_Click(object sender, EventArgs e)
        {
            Session["CursoAEditar"] = null;
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnVerCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerCurso.aspx", false);
        }

        public void listarCursosInscripto()
        {
            List<Curso> listaCursosInscriptos = CursoNegocio.listarCursosInscripto();

            repCardsCurso.DataSource = listaCursosInscriptos;
            repCardsCurso.DataBind();
            
        }
        public void listarCursosCreados() 
        {
            Usuario user = (Usuario)Session["usuario"];
            if (user != null)
            {
                List<Curso> listaCursos = CursoNegocio.listarCursosPorIdUsuario(user.Id, false);
                repCursos.DataSource = listaCursos;
                repCursos.DataBind();
            }
            else
            {
                repCursos.DataSource = null;
                repCursos.DataBind();
            }

        }
        

        protected void btnEditarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            Session["CursoAEditar"] = curso;
            Response.Redirect("NuevoCurso.aspx", false);
        }

        protected void btnActivarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnActivar"] = id;
        }

        protected void btnDesactivarCurso_Click(Object sender, EventArgs e)
        {
            int id = (int)Session["btnActivar"];

            List<Curso> listaCurso = CursoNegocio.listarCursos(false);
            Curso curso = listaCurso.Find(c => c.Id == id);

            try
            {
                if (curso.Disponible)
                    CursoNegocio.desactivarCurso(id);
                else
                    CursoNegocio.activarCurso(id);


            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
            }

            listarCursosCreados();
        }

        protected void btnEliminarCurso_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);

            Session["btnEliminar"] = id;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)Session["btnEliminar"];

                CursoNegocio cursoAEliminar = new CursoNegocio();
                cursoAEliminar.eliminarCurso(id);

                listarCursosCreados();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
            
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.CommandArgument);
            Session["idCursoCreadoSeleccionado"] = id;
            
            Response.Redirect("CapitulosCurso.aspx", false);

        }

    }
}