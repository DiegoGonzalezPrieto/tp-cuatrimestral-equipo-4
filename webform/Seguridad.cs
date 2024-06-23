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
        public static Usuario UsuarioActual
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
            if (UsuarioActual != null)
                return UsuarioActual.Tipo == TipoUsuario.Admin;

            return false;
        }

        /// <summary>
        /// Indica si el usuario logueado adquirio el curso.
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public static bool adquirioCurso(int idCurso)
        {
            if (UsuarioActual == null)
                return false;

            List<Curso> cursosAdquiridos = UsuarioNegocio.listarCursosAdquiridos(UsuarioActual.Id);

            return cursosAdquiridos.Exists(c => c.Id == idCurso);
        }

        /// <summary>
        /// Indica si el usuario logueado es creador del curso.
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public static bool creoCurso(int idCurso)
        {
            if (UsuarioActual == null)
                return false;

            List<Curso> cursosCreados = CursoNegocio.listarCursosPorIdUsuario(UsuarioActual.Id);

            return cursosCreados.Exists(c => c.Id == idCurso);
        }

        /// <summary>
        /// Indica si el usuario logueado ya agregó una reseña en este curso.
        /// </summary>
        /// <param name="idCurso"></param>
        /// <returns></returns>
        public static bool agregoResena(int idCurso)
        {
            if (UsuarioActual == null)
                return false;

            List<Resena> resenasCurso = ResenaNegocio.listarResenasDeCurso(idCurso);

            return resenasCurso.Exists(r => r.IdUsuario == UsuarioActual.Id);
        }

    }
}