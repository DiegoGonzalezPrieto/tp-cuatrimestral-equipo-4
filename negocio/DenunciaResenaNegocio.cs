using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class DenunciaResenaNegocio
    {
        public static void RegistrarDenuncia(int idResenia, int idUsuario, string mensaje)
        {

            Datos datos = new Datos();
            try
            {
                datos.setearConsulta("INSERT INTO Denuncia_Resenia (Id_Resenia, Id_UsuarioDenunciante, Mensaje, FechaCreacion, Resuelta) VALUES (@IdResenia, @IdUsuario, @Mensaje, @FechaCreacion, @Resuelta)");
                datos.setearParametro("@IdResenia", idResenia);
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@Mensaje", mensaje);
                datos.setearParametro("@FechaCreacion", DateTime.Now);
                datos.setearParametro("@Resuelta", false);

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

        public static List<DenunciaResena> ListarDenuncias(bool soloPendientes = false)
        {

            Datos datos = new Datos();

            List<DenunciaResena> denuncias = new List<DenunciaResena>();

            try
            {
                string consulta = "SELECT Id, Id_Resenia, Id_UsuarioDenunciante, Mensaje, FechaCreacion, Resuelta " +
                    " FROM Denuncia_Resenia WHERE 1 = 1 ";

                if (soloPendientes)
                    consulta += " AND Resuelta = 0 ";

                consulta += " ORDER BY Resuelta ASC ";

                datos.setearConsulta(consulta);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DenunciaResena denuncia = new DenunciaResena();

                    denuncia.IdReseña = (int)datos.Lector["Id_Resenia"];
                    denuncia.IdDenunciante = (int)datos.Lector["Id_UsuarioDenunciante"];
                    denuncia.MensajeDenuncia = (string)datos.Lector["Mensaje"];
                    denuncia.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    denuncia.Resuelta = (bool)datos.Lector["Resuelta"];
                    denuncia.Id = (int)datos.Lector["Id"];

                    denuncias.Add(denuncia);
                }

                return denuncias;
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

        public static void DenunciaResuelta(int idDenuncia)
        {
            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("UPDATE Denuncia_Resenia " +
                    " SET Resuelta = 1 WHERE Id = @idDenuncia");
                accesoDatos.setearParametro("@idDenuncia", idDenuncia);

                accesoDatos.ejecutarAccion();

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
        public static void DenunciaPendiente(int idDenuncia)
        {
            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("UPDATE Denuncia_Resenia " +
                    " SET Resuelta = 0 WHERE Id = @idDenuncia");
                accesoDatos.setearParametro("@idDenuncia", idDenuncia);

                accesoDatos.ejecutarAccion();

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
