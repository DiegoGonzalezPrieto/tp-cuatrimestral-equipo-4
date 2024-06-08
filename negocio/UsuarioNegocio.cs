using System;
using System.Collections.Generic;
using System.Linq;
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
            finally { 
                accesoDatos.cerrarConexion();
            }

       }

        public int insertarNuevo (Usuario user)
        {
            Datos accesoDatos = new Datos();
            

            try
            {
                accesoDatos.setearProcedimiento("InsertarUsuario");
                accesoDatos.setearParametro("@email",user.Correo);
                accesoDatos.setearParametro("@Pass",user.Password);
                accesoDatos.setearParametro("@Nombre",user.Nombre);

                return accesoDatos.ejecturarAccionScalar();

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

    }
}
