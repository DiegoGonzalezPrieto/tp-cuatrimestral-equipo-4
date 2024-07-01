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
        public static List<Usuario> listarUsuarios(bool activos = false)
        {
            List<Usuario> listarUsuario = new List<Usuario>();

            Datos accesoDatos = new Datos();

            try
            {
                string consulta = "SELECT Id, Email, Pass, TipoUsuario, FechaAlta, Estado, UserName,  FotoPerfil, Nombre, Apellido, Profesion, Provincia, Biografia" +
                    " FROM Usuarios INNER JOIN DatosPersonales ON Id = IdUsuario WHERE TipoUsuario = 0";

                if (activos)
                    consulta += " AND Estado = 1 ";

                accesoDatos.setearConsulta(consulta);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)accesoDatos.Lector["Id"];
                    usuario.Correo = (string)accesoDatos.Lector["Email"];
                    usuario.Password = (string)accesoDatos.Lector["Pass"];
                    
                    usuario.FechaAlta = accesoDatos.Lector["FechaAlta"] is DBNull ? DateTime.Now : (DateTime)accesoDatos.Lector["FechaAlta"];
                    usuario.Username = (string)accesoDatos.Lector["UserName"];
                    usuario.Estado = (bool)accesoDatos.Lector["Estado"];
                    usuario.UrlFotoPerfil = accesoDatos.Lector["FotoPerfil"] is DBNull ? "" : (string)accesoDatos.Lector["FotoPerfil"];
                    usuario.Nombre = accesoDatos.Lector["Nombre"] is DBNull ? "" : (string)accesoDatos.Lector["Nombre"];
                    usuario.Apellido = accesoDatos.Lector["Apellido"] is DBNull ? "" : (string)accesoDatos.Lector["Apellido"];
                    usuario.Profesion = accesoDatos.Lector["Profesion"] is DBNull ? "" : (string)accesoDatos.Lector["Profesion"];
                    usuario.Biografia = accesoDatos.Lector["Biografia"] is DBNull ? "" : (string)accesoDatos.Lector["Biografia"];
                    usuario.Provincia = accesoDatos.Lector["Provincia"] is DBNull ? "" : (string)accesoDatos.Lector["Provincia"];

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

        public static List<Usuario> listarUsuariosInscriptos(int id)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT U.UserName, U.Estado, DP.FotoPerfil " +
                    "FROM Usuarios_Cursos UC INNER JOIN Usuarios U " +
                    "ON UC.Id_Usuario = U.Id INNER JOIN Cursos C " +
                    "ON UC.Id_Curso = C.Id INNER JOIN DatosPersonales DP " +
                    "ON U.Id = DP.IdUsuario " +
                    "WHERE Id_Curso = @IdCurso";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IdCurso", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Username = (string)datos.Lector["UserName"];
                    usuario.Estado = (bool)datos.Lector["Estado"];
                    usuario.UrlFotoPerfil = (string)datos.Lector["FotoPerfil"];

                    listaUsuarios.Add(usuario);
                }

                return listaUsuarios;
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<Usuario> listarAdministradores()
        {
            List<Usuario> listarUsuario = new List<Usuario>();

            Datos accesoDatos = new Datos();

            try
            {
                string consulta = "SELECT Id, Email, Pass, TipoUsuario, FechaAlta, Estado, UserName, FotoPerfil  FROM Usuarios INNER JOIN DatosPersonales ON Id = IdUsuario WHERE TipoUsuario = 1";
                accesoDatos.setearConsulta(consulta);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)accesoDatos.Lector["Id"];
                    usuario.Correo = (string)accesoDatos.Lector["Email"];
                    usuario.Password = (string)accesoDatos.Lector["Pass"];

                    usuario.FechaAlta = accesoDatos.Lector["FechaAlta"] is DBNull ? DateTime.Now : (DateTime)accesoDatos.Lector["FechaAlta"];
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
                accesoDatos.setearParametro("@fecha", user.FechaAlta);
                
                int idUsuario = accesoDatos.ejecturarAccionScalar();
                accesoDatos.cerrarConexion();


                accesoDatos.setearConsulta("INSERT INTO DatosPersonales (IdUsuario, FotoPerfil) VALUES (@IdUsuario, '')");
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
        
        public Usuario ObtenerUsuario(int idUsuario)
        {
            Usuario user = new Usuario();

            Datos accesoDatos = new Datos();

            try
            {
                string consulta = "SELECT U.Id, U.Email, U.Pass, U.TipoUsuario, U.FechaAlta, U.Estado, U.UserName, DP.Nombre, DP.Apellido, DP.Profesion, DP.Provincia, DP.Pais, DP.FechaNacimiento, DP.Biografia, DP.FotoPerfil" +
                    " FROM Usuarios U INNER JOIN DatosPersonales DP ON Id = IdUsuario WHERE Id = @idUsuario ";

                accesoDatos.setearParametro("@idUsuario", idUsuario);

                accesoDatos.setearConsulta(consulta);
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

                }

                return user;
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

        public static DateTime? ObtenerFechaAdquisicion(int Id_Usuario, int Id_Curso)
        {
            Datos accesoDatos = new Datos();

            try
            {

                accesoDatos.setearConsulta("SELECT FechaAdquisicion FROM Usuarios_Cursos WHERE Id_Usuario = @Id_Usuario AND Id_Curso = @Id_Curso");
                accesoDatos.setearParametro("@Id_Usuario", Id_Usuario);
                accesoDatos.setearParametro("@id_Curso", Id_Curso);

                accesoDatos.ejecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    DateTime FechaAdquisicion = (DateTime)accesoDatos.Lector["FechaAdquisicion"];
                    return FechaAdquisicion;
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

        public static void BajaUsuarioCurso(int Id_Usuario, int Id_Curso) 
        {

            Datos accesoDatos = new Datos();

            try
            {

                accesoDatos.setearConsulta("UPDATE Usuarios_Cursos SET Estado = 0 WHERE Id_Usuario = @Id_Usuario AND Id_Curso = @Id_Curso");
                accesoDatos.setearParametro("@Id_Usuario", Id_Usuario);
                accesoDatos.setearParametro("@id_Curso", Id_Curso);

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

        public string BuscarEmail(string email)
        {
            Datos accesoDatos = new Datos();

            try
            {
                string pass = string.Empty;
                accesoDatos.setearConsulta("SELECT Pass FROM Usuarios WHERE Email = @Email");
                accesoDatos.setearParametro("@Email", email);

                accesoDatos.ejecutarLectura();

                if (accesoDatos.Lector.Read())
                {
                    pass = accesoDatos.Lector["Pass"] != DBNull.Value ? accesoDatos.Lector["Pass"].ToString() : string.Empty;
                }

                return pass;
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

        public void CambiarPass(int Id,string pass)
        {
            Datos accesoDatos = new Datos();

            try
            {
                accesoDatos.setearConsulta("UPDATE Usuarios SET Pass = @Pass WHERE Id = @Id");
                accesoDatos.setearParametro("@Pass", pass);
                accesoDatos.setearParametro("@Id", Id);

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

