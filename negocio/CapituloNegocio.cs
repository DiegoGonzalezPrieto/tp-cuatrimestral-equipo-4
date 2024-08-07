﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CapituloNegocio
    {

        public static Capitulo obtenerCapitulo(int id)
        {
            Capitulo capitulo = new Capitulo();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Id, Id_Curso, Nombre, Orden, FechaCreacion, Activo, Liberado " +
                    " FROM Capitulos WHERE Id = @id";

                datos.setearConsulta(consulta);
                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    capitulo.Id = (int)datos.Lector["Id"];
                    capitulo.IdCurso = (int)datos.Lector["Id_Curso"];
                    capitulo.Nombre = (string)datos.Lector["Nombre"];
                    capitulo.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    capitulo.Orden = (short)datos.Lector["Orden"];
                    capitulo.Liberado = (bool)datos.Lector["Liberado"];
                    capitulo.Activo = (bool)datos.Lector["Activo"];

                }

                return capitulo;
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



        public static Capitulo obtenerCapituloDeCurso(int idCurso, int ordenCapitulo)
        {
            Capitulo capitulo = new Capitulo();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Id, Id_Curso, Nombre, Orden, FechaCreacion, Activo, Liberado " +
                    " FROM Capitulos WHERE Id_Curso = @idCurso AND Orden = @orden AND Activo = 1";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCurso", idCurso);
                datos.setearParametro("@orden", ordenCapitulo);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    capitulo.Id = (int)datos.Lector["Id"];
                    capitulo.IdCurso = (int)datos.Lector["Id_Curso"];
                    capitulo.Nombre = (string)datos.Lector["Nombre"];
                    capitulo.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    capitulo.Orden = (short)datos.Lector["Orden"];
                    capitulo.Liberado = (bool)datos.Lector["Liberado"];
                    capitulo.Activo = (bool)datos.Lector["Activo"];
                }

                // TODO : cargar contenidos? - quizás no...

                return capitulo;
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

        public static Capitulo obtenerOrdenCapitulo(int idCurso)
        {
            Capitulo capitulo = new Capitulo();
            Datos datosCapitulo = new Datos();
            try
            {
                string consulta = "SELECT TOP(1) Ca.Orden FROM Capitulos Ca INNER JOIN Cursos Cu ON Ca.Id_Curso = Cu.Id WHERE Cu.Id = @idCurso ORDER BY Ca.Orden DESC";
                datosCapitulo.setearConsulta(consulta);
                datosCapitulo.setearParametro("@idCurso", idCurso);
                datosCapitulo.ejecutarLectura();

                if (datosCapitulo.Lector.Read())
                    capitulo.Orden = (short)datosCapitulo.Lector["Orden"];

                return capitulo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCapitulo.cerrarConexion();
            }

        }

        public int obtenerOrdenCapituloAEliminar(int idCapitulo)
        {
            Datos datosCapitulo = new Datos();
            try
            {
                string consulta = "SELECT Ca.Orden FROM Capitulos Ca WHERE Ca.Id = @idCapitulo";
                datosCapitulo.setearConsulta(consulta);
                datosCapitulo.setearParametro("@idCapitulo", idCapitulo);
                int ordenEliminar = datosCapitulo.ejecturarAccionScalar();

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
                string consulta = "UPDATE Capitulos SET Orden = @nuevoOrden WHERE Orden = @ordenActual ";
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

        public static void insertarCapitulo(int id, string nombreCapitulo, int orden)
        {
            Datos datosNuevoCapitulo = new Datos();
            try
            {

                string consulta = "INSERT INTO Capitulos (Id_Curso, Nombre, Orden, FechaCreacion, Activo, Liberado) " +
                    "VALUES (@idCurso, @nombreCapitulo, @orden, GETDATE(), 1, 0)";
                datosNuevoCapitulo.setearConsulta(consulta);
                datosNuevoCapitulo.setearParametro("@idCurso", id);
                datosNuevoCapitulo.setearParametro("@nombreCapitulo", nombreCapitulo);
                datosNuevoCapitulo.setearParametro("@orden", orden);

                datosNuevoCapitulo.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosNuevoCapitulo.cerrarConexion();
            }
        }

        public static void modificarCapitulo(Capitulo capitulo)
        {
            Datos datosModificarCapitulo = new Datos(); 
            try
            {
                string consulta = "UPDATE Capitulos SET Nombre = @Nombre, Liberado = @Liberado WHERE Id = @idCapitulo";
                datosModificarCapitulo.setearConsulta(consulta);
                datosModificarCapitulo.setearParametro("@Nombre", capitulo.Nombre);
                datosModificarCapitulo.setearParametro("@Liberado", capitulo.Liberado);
                //datosModificarCapitulo.setearParametro("@Orden", capitulo.Orden);
                datosModificarCapitulo.setearParametro("@idCapitulo", capitulo.Id);
                datosModificarCapitulo.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosModificarCapitulo.cerrarConexion();
            }
        }

        public void eliminacionLogicaCapitulo(int idCapitulo)
        {
            Datos datosCapituloModificado = new Datos();

            try
            {
                string consulta = "UPDATE Capitulos SET Activo = 0 WHERE Id = @IdCapitulo ";
                datosCapituloModificado.setearConsulta(consulta);
                datosCapituloModificado.setearParametro("@IdCapitulo", idCapitulo);
                datosCapituloModificado.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCapituloModificado.cerrarConexion();
            }
        }

        public static void cambiarOrdenCapituloNuevo(short ordenNuevo, int idCapituloNuevo)
        {
            Datos datos = new Datos();
            try
            {
                string consulta = "UPDATE Capitulos SET Orden = @OrdenNuevo WHERE Id = @IdCapituloNuevo";
                datos.setearConsulta(consulta);
                datos.setearParametro("@OrdenNuevo", ordenNuevo);
                datos.setearParametro("@IdCapituloNuevo", idCapituloNuevo);
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

        public static void cambiarOrdenCapituloViejo(short ordenActual, int idCapituloActual)
        {
            Datos datos = new Datos();
            try
            {
                string consulta = "UPDATE Capitulos SET Orden = @OrdenActual WHERE Id = @IdCapituloActual";
                datos.setearConsulta(consulta);
                datos.setearParametro("@OrdenActual", ordenActual);
                datos.setearParametro("@IdCapituloActual", idCapituloActual);
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


        public static List<Capitulo> listarCapitulos(int idCurso)
        {
            List<Capitulo> listadoCapitulo = new List<Capitulo>();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Ca.Id, Ca.Nombre, Ca.Orden, Ca.FechaCreacion, Ca.FechaCreacion, Ca.Liberado, Ca.Activo " +
                    " FROM Capitulos Ca INNER JOIN Cursos Cu ON Ca.Id_Curso = Cu.Id WHERE Cu.Id = @idCurso AND Ca.Activo = 1";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCurso", idCurso);


                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Capitulo capitulo = new Capitulo();
                    capitulo.Id = (int)datos.Lector["Id"];
                    capitulo.Nombre = (string)datos.Lector["Nombre"];
                    capitulo.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    capitulo.Orden = (short)datos.Lector["Orden"];
                    capitulo.Liberado = (bool)datos.Lector["Liberado"];
                    capitulo.Activo = (bool)datos.Lector["Activo"];

                    capitulo.Contenidos = obtenerContenidosCapitulo(capitulo.Id);

                    listadoCapitulo.Add(capitulo);
                }

                return listadoCapitulo;


            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    
        public static int cantidadDeContenidosActivos(int idCapitulo)
        {

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT COUNT(Co.Id) FROM Capitulos Ca INNER JOIN Contenidos Co ON Co.Id_Capitulo = Ca.Id " +
                    " WHERE Ca.Id = @idCapitulo AND Co.Activo = 1";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCapitulo", idCapitulo);

                return datos.ejecturarAccionScalar();

            }
            catch (Exception ex)
            {
                throw ex; ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public static List<Contenido> obtenerContenidosCapitulo(int idCapitulo, bool soloActivos = true)
        {
            List<Contenido> listaContenidos = new List<Contenido>();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Contenidos.Id, Id_Capitulo, Contenidos.Nombre, Orden, " +
                    " TipoContenido.Id AS Id_TipoContenido, TipoContenido.Nombre AS TipoContenido_Nombre, " +
                    " Texto, ArchivoPDF, FechaCreacion, Activo, Liberado, UrlVideo " +
                    " FROM Contenidos " +
                    " JOIN TipoContenido ON TipoContenido.Id = Contenidos.TipoContenido " +
                    " WHERE Contenidos.Id_Capitulo = @idCapitulo ";

                if (soloActivos)
                    consulta += " AND Activo = 1";

                consulta += " ORDER BY Orden ASC "; // para indice en orden

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
                    contenido.Texto = datos.Lector["Texto"] is DBNull ? "" : (string)datos.Lector["Texto"];
                    contenido.Archivo = datos.Lector["ArchivoPDF"] is DBNull ? new byte[0] : (byte[])datos.Lector["ArchivoPDF"];
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


                    listaContenidos.Add(contenido);
                }

                return listaContenidos;


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
