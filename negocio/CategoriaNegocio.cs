﻿using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public static List<Categoria> listarCategorias(bool soloActivas = true)
        {
            List<Categoria> listarCategoria = new List<Categoria>();

            Datos accesoDatosCategoria = new Datos();

            try
            {
                string consulta = "SELECT Id, Nombre, Imagen, Activo FROM Categorias ";
                if (soloActivas)
                    consulta += " WHERE Activo = 1";

                accesoDatosCategoria.setearConsulta(consulta);

                accesoDatosCategoria.ejecutarLectura();

                while (accesoDatosCategoria.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)accesoDatosCategoria.Lector["Id"];
                    categoria.Nombre = (string)accesoDatosCategoria.Lector["Nombre"];
                    if (accesoDatosCategoria.Lector["Imagen"] != DBNull.Value)
                    {
                        categoria.Imagen = (byte[])accesoDatosCategoria.Lector["Imagen"];
                    }
                    else
                    {
                        categoria.Imagen = null;
                    }
                    categoria.Activo = (bool)accesoDatosCategoria.Lector["Activo"];

                    listarCategoria.Add(categoria);
                }

                return listarCategoria;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosCategoria.cerrarConexion();
            }

        }

        public void agregarCategoria(Categoria nuevaCategoria)
        {

            Datos datosNuevaCategoria = new Datos();

            try
            {
                datosNuevaCategoria.setearConsulta("INSERT INTO Categorias (Nombre, Imagen, Activo) VALUES(@Nombre, @Imagen, 1)");
                datosNuevaCategoria.setearParametro("@Nombre", nuevaCategoria.Nombre);
                datosNuevaCategoria.setearParametro("@Imagen", nuevaCategoria.Imagen);
                datosNuevaCategoria.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosNuevaCategoria.cerrarConexion();
            }

        }

        public static void modificarCategoria(Categoria categoria)
        {
            Datos datosNuevaCategoria = new Datos();

            try
            {
                datosNuevaCategoria.setearConsulta("UPDATE Categorias SET Nombre = @Nombre, Imagen = @Imagen, Activo = @Activo WHERE Id = @Id");
                datosNuevaCategoria.setearParametro("@Nombre", categoria.Nombre);
                datosNuevaCategoria.setearParametro("@Imagen", categoria.Imagen);
                datosNuevaCategoria.setearParametro("@Activo", categoria.Activo);
                datosNuevaCategoria.setearParametro("@Id", categoria.Id);
                datosNuevaCategoria.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosNuevaCategoria.cerrarConexion();
            }
        }

        public static void eliminarLogicamenteCategoria(int id)
        {
            Datos datosNuevaCategoria = new Datos();

            try
            {
                datosNuevaCategoria.setearConsulta("UPDATE Categorias SET Activo = 0 WHERE Id = @Id");
                datosNuevaCategoria.setearParametro("@Id", id);
                datosNuevaCategoria.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosNuevaCategoria.cerrarConexion();
            }
        }
        public static void activarLogicamenteCategoria(int id)
        {
            Datos datosNuevaCategoria = new Datos();

            try
            {
                datosNuevaCategoria.setearConsulta("UPDATE Categorias SET Activo = 1 WHERE Id = @Id");
                datosNuevaCategoria.setearParametro("@Id", id);
                datosNuevaCategoria.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosNuevaCategoria.cerrarConexion();
            }
        }

        public static void eliminarFisicamenteCategoria(int id)
        {
            Datos datosNuevaCategoria = new Datos();

            try
            {
                datosNuevaCategoria.setearConsulta("DELETE FROM Categorias WHERE Id = @Id");
                datosNuevaCategoria.setearParametro("@Id", id);
                datosNuevaCategoria.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosNuevaCategoria.cerrarConexion();
            }
        }
    
        public static Categoria obtenerCategoria(int idCategoria)
        {
            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Id, Nombre, Imagen, Activo FROM Categorias " +
                    " WHERE Id = @idCategoria";

                datos.setearConsulta(consulta);
                datos.setearParametro("@idCategoria", idCategoria);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)datos.Lector["Id"];
                    categoria.Nombre = (string)datos.Lector["Nombre"];
                    if (datos.Lector["Imagen"] != DBNull.Value)
                    {
                        categoria.Imagen = (byte[])datos.Lector["Imagen"];
                    }
                    else
                    {
                        categoria.Imagen = null;
                    }
                    categoria.Activo = (bool)datos.Lector["Activo"];

                    return categoria;
                }

                return null;
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
