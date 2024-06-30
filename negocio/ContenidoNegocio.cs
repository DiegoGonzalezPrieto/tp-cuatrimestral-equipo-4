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
                    " Texto, ArchivoPDF, FechaCreacion, Activo, Liberado, UrlVideo " +
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
                    contenido.Texto = datos.Lector["Texto"] is DBNull ? "" : (string)datos.Lector["Texto"];
                    contenido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    contenido.Liberado = (bool)datos.Lector["Liberado"];
                    contenido.Activo = (bool)datos.Lector["Activo"];
                    contenido.UrlVideo = datos.Lector["UrlVideo"] is DBNull ? "" : (string)datos.Lector["UrlVideo"];


                    TipoContenido tc = new TipoContenido();
                    tc.Id = (int)datos.Lector["Id_TipoContenido"];
                    tc.Nombre = (string)datos.Lector["TipoContenido_Nombre"];

                    contenido.Tipo = tc;

                    if (contenido.Tipo.Nombre.ToLower() == "pdf")
                        contenido.Archivo = datos.Lector["ArchivoPDF"] is DBNull ? null : (byte[])datos.Lector["ArchivoPDF"];
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

        public static TipoContenido obteneTipoContenido(int idContenido)
        {
            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT TC.Id, TC.Nombre FROM TipoContenido TC INNER JOIN Contenidos C ON TC.Id = C.TipoContenido WHERE C.Id = @IdContenido ";

                datos.setearConsulta(consulta);

                datos.setearParametro("@IdContenido", idContenido);

                datos.ejecutarLectura();

                datos.Lector.Read();

                TipoContenido tipoContenido = new TipoContenido();
                tipoContenido.Id = (int)datos.Lector["Id"];
                tipoContenido.Nombre = (string)datos.Lector["Nombre"];

                return tipoContenido;
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

        public static List<Contenido> listaContenido(int idCapitulo, bool activo = true)
        {
            List<Contenido> listaContenido = new List<Contenido>();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Co.Id, Co.Id_Capitulo, Co.Nombre, Co.Orden, Co.TipoContenido, Co.Texto, Co.Liberado, " +
                    " Co.Activo, Co.ArchivoPDF, Co.FechaCreacion, Co.UrlVideo FROM Contenidos Co " +
                    " INNER JOIN Capitulos Ca ON Co.Id_Capitulo = Ca.Id WHERE Ca.Id = @idCapitulo ";
                if (activo)
                    consulta += "AND Co.Activo = 1";
                datos.setearConsulta(consulta);
                datos.setearParametro("@idCapitulo", idCapitulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Contenido contenido = new Contenido();
                    contenido.Id = (int)datos.Lector["Id"];
                    contenido.IdCapitulo = (int)datos.Lector["Id_Capitulo"];
                    contenido.Nombre = (string)datos.Lector["Nombre"];
                    contenido.Orden = (short)datos.Lector["Orden"];
                    //contenido.Tipo = new TipoContenido();
                    //contenido.Tipo.Id = (int)datos.Lector["TipoContenido"];
                    contenido.Texto = datos.Lector["Texto"] is DBNull ? "" : (string)datos.Lector["Texto"];
                    contenido.Archivo = datos.Lector["ArchivoPDF"] is DBNull ? new byte[0] : (byte[])datos.Lector["ArchivoPDF"];
                    contenido.Liberado = (bool)datos.Lector["Liberado"];
                    contenido.Activo = (bool)datos.Lector["Activo"];
                    contenido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    contenido.UrlVideo = datos.Lector["UrlVideo"] is DBNull ? "" : (string)datos.Lector["UrlVideo"];

                    contenido.Tipo = obteneTipoContenido(contenido.Id);

                    listaContenido.Add(contenido);
                }
                return listaContenido;
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

        public void agregarContenido(Contenido contenido)
        {
            Datos datosContenidoNuevo = new Datos();

            try
            {
                string consulta = "INSERT INTO Contenidos (Id_Capitulo, Nombre, Orden, TipoContenido, Texto, ArchivoPDF, FechaCreacion,  Activo, Liberado, UrlVideo) " +
                    "VALUES (@IdCapitulo, @NombreContenido, @Orden, @TipoContenido, @Texto, CONVERT(varbinary(max), @ArchivoPDF), GETDATE(), 1,  0, @UrlVideo)";
                datosContenidoNuevo.setearConsulta(consulta);
                datosContenidoNuevo.setearParametro("@IdCapitulo", contenido.IdCapitulo);
                datosContenidoNuevo.setearParametro("@NombreContenido", contenido.Nombre);
                datosContenidoNuevo.setearParametro("@Orden", contenido.Orden);
                datosContenidoNuevo.setearParametro("@TipoContenido", contenido.Tipo.Id);
                datosContenidoNuevo.setearParametro("@Texto", (object)contenido.Texto ?? DBNull.Value);
                datosContenidoNuevo.setearParametro("@ArchivoPDF", (object)contenido.Archivo ?? DBNull.Value);
                //datosContenidoNuevo.setearParametro("@Activo", contenido.Activo);
                //datosContenidoNuevo.setearParametro("@Liberado", contenido.Liberado); 
                datosContenidoNuevo.setearParametro("@UrlVideo", (object)contenido.UrlVideo ?? DBNull.Value);
                datosContenidoNuevo.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosContenidoNuevo.cerrarConexion();
            }
        }

        public void modificarContenido(Contenido contenido)
        {
            Datos datosContenidoModificado = new Datos();

            try
            {
                string consulta = "UPDATE Contenidos SET Nombre =  @NombreContenido, Orden =  @Orden, TipoContenido = @TipoContenido, Texto = @Texto, ArchivoPDF = CONVERT(varbinary(max), @ArchivoPDF), UrlVideo = @UrlVideo WHERE Id = @IdContenido ";
                datosContenidoModificado.setearConsulta(consulta);
                datosContenidoModificado.setearParametro("@NombreContenido", contenido.Nombre);
                datosContenidoModificado.setearParametro("@Orden", contenido.Orden);
                datosContenidoModificado.setearParametro("@TipoContenido", contenido.Tipo.Id);
                datosContenidoModificado.setearParametro("@Texto", (object)contenido.Texto ?? DBNull.Value);
                datosContenidoModificado.setearParametro("@ArchivoPDF", (object)contenido.Archivo ?? DBNull.Value);
                //datosContenidoModificado.setearParametro("@Activo", contenido.Activo);
                //datosContenidoModificado.setearParametro("@Liberado", contenido.Liberado); 
                datosContenidoModificado.setearParametro("@UrlVideo", (object)contenido.UrlVideo ?? DBNull.Value);
                datosContenidoModificado.setearParametro("@IdContenido", contenido.Id);
                datosContenidoModificado.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosContenidoModificado.cerrarConexion();
            }
        }

        public void eliminacionLogicaContenido(int idContenido)
        {
            Datos datosContenidoModificado = new Datos();

            try
            {
                string consulta = "UPDATE Contenidos SET Activo = 0 WHERE Id = @IdContenido ";
                datosContenidoModificado.setearConsulta(consulta);
                datosContenidoModificado.setearParametro("@IdContenido", idContenido);
                datosContenidoModificado.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosContenidoModificado.cerrarConexion();
            }
        }

        public static Contenido obtenerOrdenContenido(int idCapitulo)
        {
            Contenido contenido = new Contenido();
            Datos datosContenido = new Datos();
            try
            {
                string consulta = "SELECT TOP(1) Co.Orden FROM Contenidos Co INNER JOIN Capitulos Ca ON Co.Id_Capitulo = Ca.Id WHERE Ca.Id = @idCapitulo AND Co.Activo = 1 ORDER BY Co.Orden DESC";
                datosContenido.setearConsulta(consulta);
                datosContenido.setearParametro("@idCapitulo", idCapitulo);
                datosContenido.ejecutarLectura();

                if (datosContenido.Lector.Read())
                    contenido.Orden = (short)datosContenido.Lector["Orden"];

                return contenido;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int obtenerOrdenContenidoAEliminar(int idContenido)
        {
            Datos datosContenido = new Datos();
            try
            {
                string consulta = "SELECT Co.Orden FROM Contenidos Co WHERE Co.Id = @idContenido";
                datosContenido.setearConsulta(consulta);
                datosContenido.setearParametro("@idContenido", idContenido);
                int ordenEliminar = datosContenido.ejecturarAccionScalar();

                return ordenEliminar;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void cambiandoOrden(int nuevoOrden, int ordenActual)
        {
            Datos datos = new Datos();
            try
            {
                string consulta = "UPDATE Contenidos SET Orden = @nuevoOrden WHERE Orden = @ordenActual ";
                datos.setearConsulta(consulta);
                datos.setearParametro("@nuevoOrden", nuevoOrden);
                datos.setearParametro("@ordenActual", ordenActual);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void restringirContenido(int id)
        {
            Datos datosRestringirContenido = new Datos();

            try
            {
                datosRestringirContenido.setearConsulta("UPDATE Contenidos SET Liberado = 0 WHERE Id = @Id");
                datosRestringirContenido.setearParametro("@Id", id);
                datosRestringirContenido.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosRestringirContenido.cerrarConexion();
            }
        }
        public static void liberarContenido(int id)
        {
            Datos datosLiberarContenido = new Datos();

            try
            {
                datosLiberarContenido.setearConsulta("UPDATE Contenidos SET Liberado = 1 WHERE Id = @Id");
                datosLiberarContenido.setearParametro("@Id", id);
                datosLiberarContenido.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosLiberarContenido.cerrarConexion();
            }
        }

        public static void restringirContenidoDeCapitulos(int id)
        {
            Datos datosRestringirContenido = new Datos();

            try
            {
                datosRestringirContenido.setearConsulta("UPDATE Contenidos SET Liberado = 1 WHERE Id_Capitulo = @Id");
                datosRestringirContenido.setearParametro("@Id", id);
                datosRestringirContenido.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosRestringirContenido.cerrarConexion();
            }
        }
        public static void liberarContenidoDeCapitulos(int id)
        {
            Datos datosLiberarContenido = new Datos();

            try
            {
                datosLiberarContenido.setearConsulta("UPDATE Contenidos SET Liberado = 0 WHERE Id_Capitulo = @Id");
                datosLiberarContenido.setearParametro("@Id", id);
                datosLiberarContenido.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosLiberarContenido.cerrarConexion();
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
                    " Contenidos.Texto, ArchivoPDF, Contenidos.FechaCreacion, Contenidos.Activo, Contenidos.Liberado, Contenidos.UrlVideo " +
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
                    contenido.UrlVideo = datos.Lector["UrlVideo"] is DBNull ? "" : (string)datos.Lector["UrlVideo"];

                    TipoContenido tc = new TipoContenido
                    {
                        Id = (int)datos.Lector["Id_TipoContenido"],
                        Nombre = (string)datos.Lector["TipoContenido_Nombre"]
                    };

                    contenido.Tipo = tc;

                    if (contenido.Tipo.Nombre.ToLower() == "pdf")
                        contenido.Archivo = datos.Lector["ArchivoPDF"] is DBNull ? null : (byte[])datos.Lector["ArchivoPDF"];
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
            Contenido contenidoSiguiente;

            Datos datos = new Datos();

            try
            {
                Contenido contActual = obtenerContenido(idContenido);
                short ordenContenidoSiguiente = (Int16)(contActual.Orden + 1);

                contenidoSiguiente = obtenerContenidoDeCapitulo(contActual.IdCapitulo, ordenContenidoSiguiente);

                if (contenidoSiguiente.Id != 0)
                    return contenidoSiguiente;
                else
                {
                    Capitulo capituloActual = CapituloNegocio.obtenerCapitulo(contActual.IdCapitulo);
                    int idCurso = capituloActual.IdCurso;
                    int idCapituloSiguiente = CapituloNegocio.obtenerCapituloDeCurso(idCurso, capituloActual.Orden + 1).Id;


                    return obtenerContenidoDeCapitulo(idCapituloSiguiente, 1);
                }

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
        /// Busca el anterior contenido del mismo capítulo o, 
        /// si es el id ingresado es primer contenido de un capítulo,
        /// el último contenido del anterior capítulo.
        /// </summary>
        /// <param name="idContenido"></param>
        /// <returns></returns>
        public static Contenido obtenerContenidoAnterior(int idContenido)
        {
            Contenido contenidoAnterior;

            Datos datos = new Datos();

            try
            {
                Contenido contActual = obtenerContenido(idContenido);
                short ordenContenidoAnterior = (Int16)(contActual.Orden - 1);

                contenidoAnterior = obtenerContenidoDeCapitulo(contActual.IdCapitulo, ordenContenidoAnterior);

                if (contenidoAnterior.Id != 0)
                    return contenidoAnterior;
                else
                {
                    Capitulo capituloActual = CapituloNegocio.obtenerCapitulo(contActual.IdCapitulo);
                    int idCurso = capituloActual.IdCurso;
                    int idCapituloAnterior = CapituloNegocio.obtenerCapituloDeCurso(idCurso, capituloActual.Orden - 1).Id;

                    int cantidadContenidos = CapituloNegocio.cantidadDeContenidosActivos(idCapituloAnterior);
                    return obtenerContenidoDeCapitulo(idCapituloAnterior, (short)cantidadContenidos);
                }
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
        /// Revisa estado de completado para ese usuario y contenido.
        /// 1 = Completado
        /// 0 = No completado
        /// Si no hay registro de relación, devuelve -1.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idContenido"></param>
        /// <returns></returns>
        public static int obtenerContenidoCompletado(int idUsuario, int idContenido)
        {
            Datos datos = new Datos();

            try
            {
                datos.setearConsulta("SELECT Completado FROM Usuarios_Contenidos_Completados " +
                    "WHERE Id_Usuario = @idUsuario AND Id_Contenido = @idContenido");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idContenido", idContenido);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (datos.Lector["Completado"] is DBNull)
                        return 0;
                    else
                        return (bool)datos.Lector["Completado"] ? 1 : 0;
                }
                return -1;

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
        public static void marcarContenidoCompletado(int idUsuario, int idContenido, bool completado = true)
        {

            Datos datos = new Datos();

            int banderaCompletado = completado ? 1 : 0;

            try
            {
                string consulta = "";

                int yaCompletado = obtenerContenidoCompletado(idUsuario, idContenido);
                if (yaCompletado == -1)
                {
                    // no hay registro: INSERT
                    consulta = "INSERT Usuarios_Contenidos_Completados (Id_Usuario, Id_Contenido, Completado) " +
                        "VALUES (@idUsuario, @idContenido, @completado)";
                }
                else
                {
                    // actualizar registro
                    consulta = "UPDATE Usuarios_Contenidos_Completados SET Completado = @completado " +
                        " WHERE Id_Usuario = @idUsuario AND Id_Contenido = @idContenido";
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idContenido", idContenido);
                datos.setearParametro("@completado", banderaCompletado);

                datos.ejecutarAccion();
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
