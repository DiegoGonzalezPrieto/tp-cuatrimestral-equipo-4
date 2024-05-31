using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public static List<Categoria> listarCategorias()
        {
            List<Categoria> listarCategoria = new List<Categoria>();

            Datos accesoDatosCategoria = new Datos();

            try
            {
                accesoDatosCategoria.setearConsulta("SELECT Id, Nombre, Imagen, Activo FROM Categorias");

                accesoDatosCategoria.ejecutarLectura();

                while (accesoDatosCategoria.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)accesoDatosCategoria.Lector["Id"];
                    categoria.Nombre = (string)accesoDatosCategoria.Lector["Nombre"];
                    categoria.Imagen = (byte[])accesoDatosCategoria.Lector["Imagen"];
                    categoria.Activa = (bool)accesoDatosCategoria.Lector["Activo"];

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
                datosNuevaCategoria.setearParametro("@Activo", categoria.Activa);
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
    }
}
