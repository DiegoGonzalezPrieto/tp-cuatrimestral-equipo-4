using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ComentarioNegocio
    {

        public void agregarComentario(Comentario nuevoComentario)
        {

            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("INSERT INTO Comentarios (Id_Curso, Id_Usuario, Mensaje, FechaCreacion, Activo, Id_aResponder) VALUES (@IdCurso, @IdUsuario, @MensajeResena, @FechaCreacion, @Activo, @Id_aResponder)");
                accesoDatos.setearParametro("@IdCurso", nuevoComentario.IdCurso);
                accesoDatos.setearParametro("@IdUsuario", nuevoComentario.IdUsuario);
                accesoDatos.setearParametro("@MensajeResena", nuevoComentario.Mensaje);
                accesoDatos.setearParametro("@FechaCreacion", nuevoComentario.FechaCreacion);
                accesoDatos.setearParametro("@Activo", true);
                accesoDatos.setearParametro("@Id_aResponder", nuevoComentario.Id_aResponder != -1 ? nuevoComentario.Id_aResponder : (object)DBNull.Value);

                accesoDatos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        }

        public static List<Comentario> listarComentarios(int idCurso, bool soloActivas = true)
        {
            List<Comentario> listarComentarios = new List<Comentario>();

            Datos accesoDatosComentarios = new Datos();

            try
            {
                string consulta = "SELECT Comentarios.Id, id_Curso, Id_Usuario, Mensaje, FechaCreacion, Usuarios.UserName as Nombre, " +
                    " Activo, Id_aResponder FROM Comentarios " +
                    " JOIN Usuarios ON Comentarios.Id_Usuario = Usuarios.Id " + 
                    " WHERE id_curso = @idCurso AND Id_aResponder IS NULL";
                if (soloActivas)
                    consulta += " AND Activo = 1";

                accesoDatosComentarios.setearConsulta(consulta);
                accesoDatosComentarios.setearParametro("@idCurso", idCurso);

                accesoDatosComentarios.ejecutarLectura();

                while (accesoDatosComentarios.Lector.Read())
                {
                    Comentario comentario = new Comentario();
                    comentario.IdCurso = idCurso;
                    comentario.Id = (int)accesoDatosComentarios.Lector["Id"];
                    comentario.IdUsuario = (int)accesoDatosComentarios.Lector["Id_Usuario"];
                    comentario.Mensaje = (string)accesoDatosComentarios.Lector["Mensaje"];
                    comentario.FechaCreacion = (DateTime)accesoDatosComentarios.Lector["FechaCreacion"];
                    comentario.Activo = (bool)accesoDatosComentarios.Lector["Activo"];
                    comentario.NombreUsuario = (string)accesoDatosComentarios.Lector["Nombre"];
                    comentario.Id_aResponder = -1;

                    listarComentarios.Add(comentario);
                }

                return listarComentarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosComentarios.cerrarConexion();
            }

        }

        public static List<Comentario> listarRespuestas(int idCurso, int IdComentario, bool soloActivas = true)
        {
            List<Comentario> listarComentarios = new List<Comentario>();

            Datos accesoDatosComentarios = new Datos();

            try
            {
                string consulta = "SELECT Comentarios.Id, id_Curso, Id_Usuario, Mensaje, FechaCreacion, " +
                    " Activo, Id_aResponder,  Usuarios.UserName as Nombre" +
                    " FROM Comentarios JOIN Usuarios ON Comentarios.Id_Usuario = Usuarios.Id " +
                    " WHERE id_curso = @idCurso AND Id_aResponder = @IdComentario ";
                if (soloActivas)
                {
                    consulta += " AND Activo = 1";
                }

                accesoDatosComentarios.setearConsulta(consulta);
                accesoDatosComentarios.setearParametro("@idCurso", idCurso);
                accesoDatosComentarios.setearParametro("@IdComentario", IdComentario);

                accesoDatosComentarios.ejecutarLectura();

                while (accesoDatosComentarios.Lector.Read())
                {
                    Comentario comentario = new Comentario();
                    comentario.Id = (int)accesoDatosComentarios.Lector["Id"];
                    comentario.IdUsuario = (int)accesoDatosComentarios.Lector["Id_Usuario"];
                    comentario.Mensaje = (string)accesoDatosComentarios.Lector["Mensaje"];
                    comentario.FechaCreacion = (DateTime)accesoDatosComentarios.Lector["FechaCreacion"];
                    comentario.Activo = (bool)accesoDatosComentarios.Lector["Activo"];
                    comentario.Id_aResponder = (int)accesoDatosComentarios.Lector["Id_aResponder"];
                    comentario.NombreUsuario = (string)accesoDatosComentarios.Lector["Nombre"];

                    listarComentarios.Add(comentario);
                }

                return listarComentarios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosComentarios.cerrarConexion();
            }

        }
    }
}
