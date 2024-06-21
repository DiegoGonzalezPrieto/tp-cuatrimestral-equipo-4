using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EstadisticaNegocio
    {
        public static string CantidadCurso()
        {
            Datos datosCantidadCurso = new Datos();
            try
            {
                string consulta = "SELECT COUNT(*) AS CantidadCursos FROM Cursos";

                datosCantidadCurso.setearConsulta(consulta);
                datosCantidadCurso.ejecutarLectura();

                datosCantidadCurso.Lector.Read();

                string cantidadCurso = datosCantidadCurso.Lector["CantidadCursos"].ToString();
                
                return cantidadCurso;
             

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCantidadCurso.cerrarConexion();
            }
            
        }

        public static string CantidadUsuarios()
        {
            Datos datosCantidadUsuarios = new Datos();
            try
            {
                string consulta = "SELECT COUNT(ID) AS 'CantidadUsuarios' FROM Usuarios WHERE TipoUsuario = 0";

                datosCantidadUsuarios.setearConsulta(consulta);
                datosCantidadUsuarios.ejecutarLectura();

                datosCantidadUsuarios.Lector.Read();

                string cantidadUsuarios = datosCantidadUsuarios.Lector["CantidadUsuarios"].ToString();

                return cantidadUsuarios;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCantidadUsuarios.cerrarConexion();
            }

        }
    }
}
