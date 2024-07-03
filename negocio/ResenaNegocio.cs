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
                accesoDatos.setearParametro("@IdUsuario", nuevaResena.Usuario.Id);
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
                    resena.Usuario = new Usuario();
                    resena.Id = (int)accesoDatosResenas.Lector["Id"];
                    resena.IdCurso = (int)accesoDatosResenas.Lector["id_Curso"];
                    resena.Usuario.Id = (int)accesoDatosResenas.Lector["Id_Usuario"];
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
                string consulta = "SELECT R.Id, R.id_Curso, R.Id_Usuario, R.Puntaje, R.Mensaje, R.FechaCreacion, R.Activo, U.UserName FROM Resenia R JOIN Usuarios U ON R.Id_Usuario = U.Id WHERE id_curso = @idCurso";
                if (soloActivas)
                    consulta += " AND Activo = 1";

                accesoDatosResenas.setearConsulta(consulta);
                accesoDatosResenas.setearParametro("@idCurso", idCurso);

                accesoDatosResenas.ejecutarLectura();

                while (accesoDatosResenas.Lector.Read())
                {
                    Resena resena = new Resena();
                    resena.Id = (int)accesoDatosResenas.Lector["Id"];
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)accesoDatosResenas.Lector["Id_Usuario"];
                    usuario.Username = (string)accesoDatosResenas.Lector["UserName"];
                    resena.Usuario = usuario;
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
        public static Resena obtenerResena(int id)
        {

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Id, id_Curso, Id_Usuario, Puntaje, Mensaje, FechaCreacion, Activo " +
                    " FROM Resenia WHERE Id = @id ";

                datos.setearParametro("@id", id);
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();

                Resena resena = new Resena();
                while (datos.Lector.Read())
                {
                    resena.Usuario = new Usuario();
                    resena.Id = (int)datos.Lector["Id"];
                    resena.IdCurso = (int)datos.Lector["id_Curso"];
                    resena.Usuario.Id = (int)datos.Lector["Id_Usuario"];
                    resena.Puntaje = (short)datos.Lector["Puntaje"];
                    resena.Mensaje = (string)datos.Lector["Mensaje"];
                    resena.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    resena.Activa = (bool)datos.Lector["Activo"];

                }

                return resena;
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
