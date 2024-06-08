using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace webform
{
    public static class Seguridad
    {


        /// <summary>
        /// Devuelve los datos del usuario logueado. O null si no hay usuario.
        /// </summary>
        /// <returns></returns>
        public static Usuario UsuarioAcual
        {
            get
            {
                if (HttpContext.Current.Session["usuario"] != null)
                {
                    return HttpContext.Current.Session["usuario"] as Usuario;
                }
                return null;
            }
        }

        /// <summary>
        /// Devuelve true si el usuario logueado es admin. Si no hay usuario logueado también devuelve false.
        /// </summary>
        /// <returns></returns>
        public static bool esAdmin()
        {
            if (UsuarioAcual != null)
                return UsuarioAcual.Tipo == TipoUsuario.Admin;

            return false;
        }

        /// <summary>
        /// Indica si el usuario logueado adquirio el curso.
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public static bool adquirioCurso(int idCurso)
        {
            if (UsuarioAcual == null)
                return false;

            List<Curso> cursosAdquiridos = UsuarioNegocio.listarCursosAdquiridos(UsuarioAcual.Id);

            return cursosAdquiridos.Exists(c => c.Id == idCurso);
        }

        /// <summary>
        /// Indica si el usuario logueado es creador del curso.
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public static bool creoCurso(int idCurso)
        {
            if (UsuarioAcual == null)
                return false;

            List<Curso> cursosCreados = CursoNegocio.listarCursosPorIdUsuario(UsuarioAcual.Id);

            return cursosCreados.Exists(c => c.Id == idCurso);
        }

    }
}