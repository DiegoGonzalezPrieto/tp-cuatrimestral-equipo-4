using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ContenidoNegocio
    {
        public static Contenido obtenerContenido(int id)
        {
            Contenido contenido = new Contenido();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Contenidos.Id, Id_Capitulo, Contenidos.Nombre, Orden, " +
                    " TipoContenido.Id AS Id_TipoContenido, TipoContenido.Nombre AS TipoContenido_Nombre, " +
                    " Texto, ArchivoPDF, FechaCreacion, Activo, Liberado " +
                    " FROM Contenidos " +
                    " JOIN TipoContenido ON TipoContenido.Id = Contenidos.TipoContenido " +
                    " WHERE Contenidos.Id = @id ";

                datos.setearConsulta(consulta);
                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    contenido.Id = (int)datos.Lector["Id"];
                    contenido.IdCapitulo = (int)datos.Lector["Id_Capitulo"];
                    contenido.Nombre = (string)datos.Lector["Nombre"];
                    contenido.Orden = (short)datos.Lector["Orden"];
                    contenido.Texto = (string)datos.Lector["Texto"];
                    contenido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    contenido.Liberado = (bool)datos.Lector["Liberado"];
                    contenido.Activo = (bool)datos.Lector["Activo"];

                    TipoContenido tc = new TipoContenido();
                    tc.Id = (int)datos.Lector["Id_TipoContenido"];
                    tc.Nombre = (string)datos.Lector["TipoContenido_Nombre"];

                    contenido.Tipo = tc;

                    if (contenido.Tipo.Nombre.ToLower() == "pdf")
                        contenido.Archivo = (byte[])datos.Lector["ArchivoPDF"];
                }

                return contenido;
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

        public static List<Contenido> listaContenido(int idCapitulo)
        {
            List<Contenido> listaContenido = new List<Contenido>();

            Datos datosListaContenido = new Datos();

            try
            {
                string consulta = "SELECT Co.Id, Co.Nombre, Co.Orden, Co.TipoContenido, Co.Texto, Co.Liberado, Co.Activo, Co.ArchivoPDF, Co.FechaCreacion FROM Contenidos Co INNER JOIN Capitulos Ca ON Co.Id_Capitulo = Ca.Id WHERE Ca.Id = @idCapitulo";
                datosListaContenido.setearConsulta(consulta);
                datosListaContenido.setearParametro("@idCapitulo", idCapitulo);
                datosListaContenido.ejecutarLectura();

                while (datosListaContenido.Lector.Read())
                {
                    Contenido contenido = new Contenido();
                    contenido.Id = (int)datosListaContenido.Lector["Id"];
                    contenido.Nombre = (string)datosListaContenido.Lector["Nombre"];
                    contenido.Orden = (short)datosListaContenido.Lector["Orden"];
                    contenido.Tipo = new TipoContenido();
                    contenido.Tipo.Id = (int)datosListaContenido.Lector["TipoContenido"];
                    contenido.Texto = (string)datosListaContenido.Lector["Texto"];
                    contenido.Liberado = (bool)datosListaContenido.Lector["Liberado"];
                    contenido.Activo = (bool)datosListaContenido.Lector["Activo"];
                    //contenido.Archivo = (byte[])datosListaContenido.Lector["ArchivoPDF"];
                    contenido.FechaCreacion = (DateTime)datosListaContenido.Lector["FechaCreacion"];

                    listaContenido.Add(contenido);
                }
                return listaContenido;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static Contenido obtenerContenidoDeCapitulo(int idCapitulo, short ordenContenido)
        {
            Contenido contenido = new Contenido();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Contenidos.Id, Id_Capitulo, Contenidos.Nombre, Contenidos.Orden, " +
                    " TipoContenido.Id AS Id_TipoContenido, TipoContenido.Nombre AS TipoContenido_Nombre, " +
                    " Contenidos.Texto, ArchivoPDF, Contenidos.FechaCreacion, Contenidos.Activo, Contenidos.Liberado " +
                    " FROM Contenidos " +
                    " JOIN TipoContenido ON TipoContenido.Id = Contenidos.TipoContenido " +
                    " JOIN Capitulos ON Id_Capitulo = Capitulos.Id " +
                    " WHERE Capitulos.Id = @idCapitulo " +
                    " AND Contenidos.Orden = @ordenContenido";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCapitulo", idCapitulo);
                datos.setearParametro("@ordenContenido", ordenContenido);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    contenido.Id = (int)datos.Lector["Id"];
                    contenido.IdCapitulo = (int)datos.Lector["Id_Capitulo"];
                    contenido.Nombre = (string)datos.Lector["Nombre"];
                    contenido.Orden = (short)datos.Lector["Orden"];
                    contenido.Texto = (string)datos.Lector["Texto"];
                    contenido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    contenido.Liberado = (bool)datos.Lector["Liberado"];
                    contenido.Activo = (bool)datos.Lector["Activo"];

                    TipoContenido tc = new TipoContenido
                    {
                        Id = (int)datos.Lector["Id_TipoContenido"],
                        Nombre = (string)datos.Lector["TipoContenido_Nombre"]
                    };

                    contenido.Tipo = tc;

                    if (contenido.Tipo.Nombre.ToLower() == "pdf")
                        contenido.Archivo = (byte[])datos.Lector["ArchivoPDF"];
                }

                return contenido;
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

        /// <summary>
        /// Busca el siguiente contenido del mismo capítulo o, 
        /// si es el id ingresado es último contenido de un capítulo,
        /// el primer contenido del próximo capítulo.
        /// </summary>
        /// <param name="idContenido"></param>
        /// <returns></returns>
        public static Contenido obtenerContenidoSiguiente(int idContenido)
        {
            Contenido contenidoSiguiente = new Contenido();

            Datos datos = new Datos();

            try
            {
                Contenido contActual = obtenerContenido(idContenido);
                short ordenContenidoSiguiente = (Int16)(contActual.Orden + 1);

                contenidoSiguiente = obtenerContenidoDeCapitulo(contActual.IdCapitulo, ordenContenidoSiguiente);

                if (contenidoSiguiente.Id != 0)
                    return contenidoSiguiente;
                else
                    return obtenerContenidoDeCapitulo(contActual.IdCapitulo + 1, 1);
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
