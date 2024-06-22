using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ResenaNegocio
    {

        public void agregarResena(Resena nuevaResena)
        {

            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("INSERT INTO Resenia (Id_Curso, Id_Usuario, Puntaje, Mensaje, FechaCreacion, Activo) VALUES (@IdCurso, @IdUsuario, @Puntaje, @MensajeResena, @FechaCreacion, @Activo)");
                accesoDatos.setearParametro("@IdCurso", nuevaResena.IdCurso);
                accesoDatos.setearParametro("@IdUsuario", nuevaResena.IdUsuario);
                accesoDatos.setearParametro("@Puntaje", nuevaResena.Puntaje);
                accesoDatos.setearParametro("@MensajeResena", nuevaResena.Mensaje);
                accesoDatos.setearParametro("@FechaCreacion", nuevaResena.FechaCreacion);
                accesoDatos.setearParametro("@Activo", true);

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

            public static List<Resena> listarResenas(bool soloActivas = true)
            {
            List<Resena> listarResenas = new List<Resena>();

            Datos accesoDatosResenas = new Datos();

            try
            {
                string consulta = "SELECT Id, id_Curso, Id_Usuario, Puntaje, Mensaje, FechaCreacion, Activo " +
                    " FROM Resenia WHERE 1 = 1 ";
                if (soloActivas)
                    consulta += " AND Activo = 1";

                accesoDatosResenas.setearConsulta(consulta);

                accesoDatosResenas.ejecutarLectura();

                while (accesoDatosResenas.Lector.Read())
                {
                    Resena resena = new Resena();
                    resena.Id = (int)accesoDatosResenas.Lector["Id"];
                    resena.IdCurso = (int)accesoDatosResenas.Lector["id_Curso"];
                    resena.IdUsuario = (int)accesoDatosResenas.Lector["Id_Usuario"];
                    resena.Puntaje = (short)accesoDatosResenas.Lector["Puntaje"];
                    resena.Mensaje = (string)accesoDatosResenas.Lector["Mensaje"];
                    resena.FechaCreacion = (DateTime)accesoDatosResenas.Lector["FechaCreacion"];
                    resena.Activa = (bool)accesoDatosResenas.Lector["Activo"];                 

                    listarResenas.Add(resena);
                }

                return listarResenas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosResenas.cerrarConexion();
            }

        }

        
        public static List<Resena> listarResenasDeCurso(int idCurso, bool soloActivas = true)
            {
            List<Resena> listarResenas = new List<Resena>();

            Datos accesoDatosResenas = new Datos();

            try
            {
                string consulta = "SELECT Id, id_Curso, Id_Usuario, Puntaje, Mensaje, FechaCreacion, Activo FROM Resenia WHERE id_curso = @idCurso";
                if (soloActivas)
                    consulta += " AND Activo = 1";

                accesoDatosResenas.setearConsulta(consulta);
                accesoDatosResenas.setearParametro("@idCurso", idCurso);

                accesoDatosResenas.ejecutarLectura();

                while (accesoDatosResenas.Lector.Read())
                {
                    Resena resena = new Resena();
                    resena.Id = (int)accesoDatosResenas.Lector["Id"];
                    resena.IdUsuario = (int)accesoDatosResenas.Lector["Id_Usuario"];
                    resena.Puntaje = (short)accesoDatosResenas.Lector["Puntaje"];
                    resena.Mensaje = (string)accesoDatosResenas.Lector["Mensaje"];
                    resena.FechaCreacion = (DateTime)accesoDatosResenas.Lector["FechaCreacion"];
                    resena.Activa = (bool)accesoDatosResenas.Lector["Activo"];                 

                    listarResenas.Add(resena);
                }

                return listarResenas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosResenas.cerrarConexion();
            }

        }

        public static int puntajeResenasDeCurso(int idCurso, bool soloActivas = true)
        {
            Datos accesoDatosResenas = new Datos();

            try
            {
                string consulta = "SELECT COALESCE(SUM(Puntaje),0) AS 'Puntaje' FROM Resenia WHERE Id_Curso = @idCurso";
                if (soloActivas)
                    consulta += " AND Activo = 1";

                accesoDatosResenas.setearConsulta(consulta);
                accesoDatosResenas.setearParametro("@idCurso", idCurso);
                accesoDatosResenas.ejecutarLectura();

                accesoDatosResenas.Lector.Read();

                int puntajeResenia = (int)accesoDatosResenas.Lector["Puntaje"];

                return puntajeResenia;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosResenas.cerrarConexion();
            }

        }
    }
}
