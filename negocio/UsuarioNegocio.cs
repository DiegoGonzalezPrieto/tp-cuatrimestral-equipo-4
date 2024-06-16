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
        public List<Usuario> listarUsuarios()
        {
            List<Usuario> listarUsuario = new List<Usuario>();

            Datos accesoDatos = new Datos();

            try
            {
                //accesoDatos.setearConsulta()

                accesoDatos.ejecutarLectura();



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
                accesoDatos.setearParametro("@username", user.Nombre);
                
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
                accesoDatos.setearConsulta("Select id, email, pass, nombre, tipoUsuario, fechaAlta, estado from USUARIOS Where email = @email And pass = @pass");
                accesoDatos.setearParametro("@email", user.Correo);
                accesoDatos.setearParametro("@pass", user.Password);
                accesoDatos.ejecutarLectura();

                while (accesoDatos.Lector.Read())
                {
                    user.Id = (int)accesoDatos.Lector["Id"];
                    user.Nombre = (string)accesoDatos.Lector["Nombre"];
                    user.Tipo = (bool)accesoDatos.Lector["tipoUsuario"] ? TipoUsuario.Usuario : TipoUsuario.Admin;
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

            Datos datos = new Datos();
            try
            {
                datos.setearConsulta("Select id, email, pass, nombre, tipoUsuario, fechaAlta, estado from USUARIOS Where email = @email");
                datos.setearParametro("@email", email);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    user.Id = (int)datos.Lector["Id"];
                    user.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["fechaAlta"] is DBNull))
                        user.FechaAlta = (DateTime)datos.Lector["fechaAlta"];
                    user.Tipo = (bool)datos.Lector["tipoUsuario"] ? TipoUsuario.Admin : TipoUsuario.Usuario;
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
                datos.cerrarConexion();
            }
        }
    }
}
