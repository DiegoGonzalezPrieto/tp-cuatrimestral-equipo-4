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

        public static string CantidadCursoActivos()
        {
            Datos datosCantidadCurso = new Datos();
            try
            {
                string consulta = "SELECT COUNT(*) AS CantidadCursosActivo FROM Cursos WHERE Estado = 1";

                datosCantidadCurso.setearConsulta(consulta);
                datosCantidadCurso.ejecutarLectura();

                datosCantidadCurso.Lector.Read();

                string cantidadCurso = datosCantidadCurso.Lector["CantidadCursosActivo"].ToString();

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
        public static string CantidadUsuariosActivos()
        {
            Datos datosCantidadUsuarios = new Datos();
            try
            {
                string consulta = "SELECT COUNT(ID) AS 'CantidadUsuariosActivo' FROM Usuarios WHERE TipoUsuario = 0 AND Estado  = 1";

                datosCantidadUsuarios.setearConsulta(consulta);
                datosCantidadUsuarios.ejecutarLectura();

                datosCantidadUsuarios.Lector.Read();

                string cantidadUsuarios = datosCantidadUsuarios.Lector["CantidadUsuariosActivo"].ToString();

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

        public static int InscripcionesTotales()
        {
            Datos datosInscripciones = new Datos();
            try
            {
                string consulta = "SELECT ISNULL(SUM(CantidadAdquisiciones), 0) AS 'Inscripciones' FROM Estadisticas_Cursos";

                datosInscripciones.setearConsulta(consulta);
                datosInscripciones.ejecutarLectura();

                datosInscripciones.Lector.Read();

                int inscripciones = (int)datosInscripciones.Lector["Inscripciones"];

                return inscripciones;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosInscripciones.cerrarConexion();
            }

        }

        public static string Certificaciones()
        {
            Datos datosCertificaciones = new Datos();
            try
            {
                string consulta = "SELECT COUNT(Completado) AS 'Certificados' FROM Usuarios_Contenidos_Completados";

                datosCertificaciones.setearConsulta(consulta);
                datosCertificaciones.ejecutarLectura();

                datosCertificaciones.Lector.Read();

                string certificaciones = datosCertificaciones.Lector["Certificados"].ToString();

                return certificaciones;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCertificaciones.cerrarConexion();
            }

        }

        public static string InscriptosPorCurso(int idCurso)
        {
            Datos datosInscriptos = new Datos();

            try
            {
                string consulta = "SELECT CantidadAdquisiciones FROM Estadisticas_Cursos WHERE Id_Curso = @idCurso";

                datosInscriptos.setearConsulta(consulta);
                datosInscriptos.setearParametro("idCurso", idCurso);
                datosInscriptos.ejecutarLectura();

                datosInscriptos.Lector.Read();

                string cantidadInscriptos = datosInscriptos.Lector["CantidadAdquisiciones"].ToString();

                return cantidadInscriptos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
