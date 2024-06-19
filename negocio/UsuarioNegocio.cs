using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public static List<Usuario> listarUsuarios()
        {
            List<Usuario> listarUsuario = new List<Usuario>();

            Datos accesoDatos = new Datos();

            try
            {
                string consulta = "SELECT Id, Email, Pass, TipoUsuario, FechaAlta, Estado, UserName, FotoPerfil  FROM Usuarios INNER JOIN DatosPersonales ON Id = IdUsuario WHERE TipoUsuario = 0";
                accesoDatos.setearConsulta(consulta);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)accesoDatos.Lector["Id"];
                    usuario.Correo = (string)accesoDatos.Lector["Email"];
                    usuario.Password = (string)accesoDatos.Lector["Pass"];
                    //usuario.Tipo = (TipoUsuario)accesoDatos.Lector["TipoUsuario"];
                    //usuario.FechaAlta = (DateTime)accesoDatos.Lector["FechaAlta"];
                    usuario.Username = (string)accesoDatos.Lector["UserName"];
                    usuario.Estado = (bool)accesoDatos.Lector["Estado"];
                    usuario.UrlFotoPerfil = accesoDatos.Lector["FotoPerfil"] is DBNull ? "" : (string)accesoDatos.Lector["FotoPerfil"];

                    listarUsuario.Add(usuario);
                }

                return listarUsuario;
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

        public int insertarNuevo(Usuario user)
        {
            Datos accesoDatos = new Datos();


            try
            {
                accesoDatos.setearProcedimiento("insertarUsuario");
                accesoDatos.setearParametro("@email", user.Correo);
                accesoDatos.setearParametro("@pass", user.Password);
                accesoDatos.setearParametro("@username", user.Username);
                
                int idUsuario = accesoDatos.ejecturarAccionScalar();
                accesoDatos.cerrarConexion();


                accesoDatos.setearConsulta("INSERT INTO DatosPersonales (IdUsuario) VALUES (@IdUsuario)");
                accesoDatos.setearParametro("@IdUsuario", idUsuario);

                accesoDatos.ejecutarAccion();

                return idUsuario;

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

        public bool Login(Usuario user)
        {
            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("Select U.id, U.email, U.pass, U.username, U.tipoUsuario, U.fechaAlta, U.estado, DP.Nombre, DP.Apellido, DP.Profesion, DP.Provincia, DP.Pais, DP.FechaNacimiento, DP.FotoPerfil, DP.Biografia from USUARIOS AS U JOIN DatosPersonales AS DP ON DP.IdUsuario = U.Id WHERE email = @email And pass = @pass");
                accesoDatos.setearParametro("@email", user.Correo);
                accesoDatos.setearParametro("@pass", user.Password);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    user.Id = (int)accesoDatos.Lector["Id"];
                    user.Correo = (string)accesoDatos.Lector["Email"];
                    user.Password = (string)accesoDatos.Lector["Pass"];
                    user.Username = (string)accesoDatos.Lector["UserName"];
                    user.Tipo = (bool)accesoDatos.Lector["TipoUsuario"] ? TipoUsuario.Usuario : TipoUsuario.Admin;
                    user.FechaAlta = accesoDatos.Lector["FechaAlta"] != DBNull.Value ? (DateTime)accesoDatos.Lector["FechaAlta"] : DateTime.MinValue;
                    user.Estado = (bool)accesoDatos.Lector["Estado"];
                    user.Nombre = accesoDatos.Lector["Nombre"] != DBNull.Value ? (string)accesoDatos.Lector["Nombre"] : string.Empty;
                    user.Apellido = accesoDatos.Lector["Apellido"] != DBNull.Value ? (string)accesoDatos.Lector["Apellido"] : string.Empty;
                    user.Profesion = accesoDatos.Lector["Profesion"] != DBNull.Value ? (string)accesoDatos.Lector["Profesion"] : string.Empty;
                    user.Provincia = accesoDatos.Lector["Provincia"] != DBNull.Value ? (string)accesoDatos.Lector["Provincia"] : string.Empty;
                    user.Pais = accesoDatos.Lector["Pais"] != DBNull.Value ? (string)accesoDatos.Lector["Pais"] : string.Empty;
                    user.FechaNacimiento = accesoDatos.Lector["FechaNacimiento"] != DBNull.Value ? (DateTime)accesoDatos.Lector["FechaNacimiento"] : DateTime.MinValue;
                    user.UrlFotoPerfil = accesoDatos.Lector["FotoPerfil"] != DBNull.Value ? (string)accesoDatos.Lector["FotoPerfil"] : string.Empty;
                    user.Biografia = accesoDatos.Lector["Biografia"] != DBNull.Value ? (string)accesoDatos.Lector["Biografia"] : string.Empty;

                    return true;
                }
                return false;

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

        public void InscribirCurso(int idCurso, int idUsuario, DateTime fechaAdquisicion, bool adquisicionConfirmada, bool estado)
        {
            Datos accesoDatos = new Datos();

            try
            {
                accesoDatos.setearConsulta("INSERT INTO Usuarios_Cursos (Id_Curso, Id_Usuario, FechaAdquisicion, AdquisicionConfirmada, Estado) " +
                                           "VALUES (@IdCurso, @IdUsuario, @FechaAdquisicion, @AdquisicionConfirmada, @Estado)");
                accesoDatos.setearParametro("@IdCurso", idCurso);
                accesoDatos.setearParametro("@IdUsuario", idUsuario);
                accesoDatos.setearParametro("@FechaAdquisicion", fechaAdquisicion);
                accesoDatos.setearParametro("@AdquisicionConfirmada", adquisicionConfirmada);
                accesoDatos.setearParametro("@Estado", estado);

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

        public static List<Curso> listarCursosAdquiridos(int idUsuario, bool adquisicionConfirmada = true)
        {
            Datos datos = new Datos();

            List<Curso> cursos = new List<Curso>();

            string consulta = "SELECT Id_Curso from Usuarios_Cursos WHERE Id_Usuario = @idUsuario ";

            if (adquisicionConfirmada)
                consulta += " AND AdquisicionConfirmada = 1 ";

            try
            {

                datos.setearConsulta(consulta);
                datos.setearParametro("@idUsuario", idUsuario);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Curso c = CursoNegocio.obtenerCurso((int)datos.Lector["Id_Curso"]);
                    cursos.Add(c);
                }

                return cursos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public static Usuario obtenerPorCorreo(string email)
        {
            Usuario user = new Usuario();
            user.Correo = email;

            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("Select U.id, U.email, U.pass, U.username, U.tipoUsuario, U.fechaAlta, U.estado, DP.Nombre, DP.Apellido, DP.Profesion, DP.Provincia, DP.Pais, DP.FechaNacimiento, DP.FotoPerfil, DP.Biografia from USUARIOS AS U JOIN DatosPersonales AS DP ON DP.IdUsuario = U.Id WHERE U.email = @email");
                accesoDatos.setearParametro("@email", email);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    user.Id = (int)accesoDatos.Lector["Id"];
                    user.Correo = (string)accesoDatos.Lector["Email"];
                    user.Password = (string)accesoDatos.Lector["Pass"];
                    user.Username = (string)accesoDatos.Lector["UserName"];
                    user.Tipo = (bool)accesoDatos.Lector["TipoUsuario"] ? TipoUsuario.Admin : TipoUsuario.Usuario;
                    user.FechaAlta = accesoDatos.Lector["FechaAlta"] != DBNull.Value ? (DateTime)accesoDatos.Lector["FechaAlta"] : DateTime.MinValue;
                    user.Estado = (bool)accesoDatos.Lector["Estado"];
                    user.Nombre = accesoDatos.Lector["Nombre"] != DBNull.Value ? (string)accesoDatos.Lector["Nombre"] : string.Empty;
                    user.Apellido = accesoDatos.Lector["Apellido"] != DBNull.Value ? (string)accesoDatos.Lector["Apellido"] : string.Empty;
                    user.Profesion = accesoDatos.Lector["Profesion"] != DBNull.Value ? (string)accesoDatos.Lector["Profesion"] : string.Empty;
                    user.Provincia = accesoDatos.Lector["Provincia"] != DBNull.Value ? (string)accesoDatos.Lector["Provincia"] : string.Empty;
                    user.Pais = accesoDatos.Lector["Pais"] != DBNull.Value ? (string)accesoDatos.Lector["Pais"] : string.Empty;
                    user.FechaNacimiento = accesoDatos.Lector["FechaNacimiento"] != DBNull.Value ? (DateTime)accesoDatos.Lector["FechaNacimiento"] : DateTime.MinValue;
                    user.UrlFotoPerfil = accesoDatos.Lector["FotoPerfil"] != DBNull.Value ? (string)accesoDatos.Lector["FotoPerfil"] : string.Empty;
                    user.Biografia = accesoDatos.Lector["Biografia"] != DBNull.Value ? (string)accesoDatos.Lector["Biografia"] : string.Empty;

                    return user;
                }
                return null;

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

        public void modificarDatosPersonales(Usuario userDP)
        {
            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("UPDATE DatosPersonales " +
                    "SET Nombre = @Nombre, Apellido = @Apellido, Profesion = @Profesion, Provincia = @Provincia, Pais = @Pais, FechaNacimiento = @FechaNacimiento, FotoPerfil = @FotoPerfil, Biografia = @Biografia " +
                    "WHERE IdUsuario = @IdUsuario");


                accesoDatos.setearParametro("@IdUsuario", userDP.Id);
                accesoDatos.setearParametro("@Nombre", userDP.Nombre);
                accesoDatos.setearParametro("@Apellido", userDP.Apellido);
                accesoDatos.setearParametro("@Profesion", userDP.Profesion);
                accesoDatos.setearParametro("@Provincia", userDP.Provincia);
                accesoDatos.setearParametro("@Pais", userDP.Pais);
                accesoDatos.setearParametro("@FechaNacimiento", userDP.FechaNacimiento == default(DateTime) ? DBNull.Value : (object)userDP.FechaNacimiento);
                accesoDatos.setearParametro("@FotoPerfil", userDP.UrlFotoPerfil);
                accesoDatos.setearParametro("@Biografia", userDP.Biografia);

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
    }
}
