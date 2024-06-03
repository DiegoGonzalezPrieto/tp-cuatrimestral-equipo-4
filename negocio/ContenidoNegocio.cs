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
    }
}
