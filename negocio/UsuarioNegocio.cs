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
           

    }
}
