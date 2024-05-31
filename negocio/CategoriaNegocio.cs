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



    }
}
