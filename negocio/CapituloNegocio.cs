﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
                string consulta = "SELECT Id, Nombre, Orden, FechaCreacion, Activo, Liberado " +
                    " FROM Capitulos WHERE Id_Curso = @idCurso AND Orden = @orden";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCurso", idCurso);
                datos.setearParametro("@orden", ordenCapitulo);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    capitulo.Id = (int)datos.Lector["Id"];
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

        public static void insertarCapitulo(int id, string nombreCapitulo)
        {
            Datos datosNuevoCapitulo = new Datos();
            try
            {
                
                string consulta = "INSERT INTO Capitulos (Id_Curso, Nombre, Orden, FechaCreacion, Activo, Liberado) " +
                    "VALUES (@idCurso, @nombreCapitulo, 3, GETDATE(), 1, 1)";
                datosNuevoCapitulo.setearConsulta(consulta);
                datosNuevoCapitulo.setearParametro("@idCurso", id);
                datosNuevoCapitulo.setearParametro("@nombreCapitulo", nombreCapitulo);
                
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

        public static List<Capitulo> listarCapitulos(int id)
        {
            List<Capitulo> listadoCapitulo = new List<Capitulo>();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Ca.Id, Ca.Nombre, Ca.Orden, Ca.FechaCreacion, Ca.FechaCreacion, Ca.Liberado, Ca.Activo FROM Capitulos Ca INNER JOIN Cursos Cu ON Ca.Id_Curso = Cu.Id WHERE Cu.Id = @idCurso";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCurso", id);


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

                    listadoCapitulo.Add(capitulo);
                }

                return listadoCapitulo;


            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
    }
}
