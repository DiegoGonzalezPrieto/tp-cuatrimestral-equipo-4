using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class TipoContenidoNegocio
    {
        public static List<TipoContenido> listaTipoContenido()
        {
            List<TipoContenido> listaTipoContenidos = new List<TipoContenido>();

            Datos datosListaTipoContenido = new Datos();

            try
            {
                string consulta = "SELECT Id, Nombre FROM TipoContenido";

                datosListaTipoContenido.setearConsulta(consulta);
                datosListaTipoContenido.ejecutarLectura();

                while (datosListaTipoContenido.Lector.Read())
                {
                    TipoContenido tipoContenido = new TipoContenido();
                    tipoContenido.Id = (int)datosListaTipoContenido.Lector["Id"];
                    tipoContenido.Nombre = (string)datosListaTipoContenido.Lector["Nombre"];

                    listaTipoContenidos.Add(tipoContenido);
                }

                return listaTipoContenidos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosListaTipoContenido.cerrarConexion();
            }
        }

    }
}
