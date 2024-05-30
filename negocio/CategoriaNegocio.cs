using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listarCategorias()
        {
            List<Categoria> listarCategoria = new List<Categoria>();

            Datos accesoDatos = new Datos();

            try
            {
                //accesoDatos.setearConsulta()

                accesoDatos.ejecutarLectura();



                return listarCategoria;
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



    }
}
