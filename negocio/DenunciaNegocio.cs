using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DenunciaNegocio
    {
        public void RegistrarDenuncia(int idCurso, int idUsuario, string mensajeDenuncia)
        {
           
            Datos accesoDatos = new Datos();
            try
            {
                accesoDatos.setearConsulta("INSERT INTO Denuncia_Cursos (Id_Curso, Id_UsuarioDenunciante, MensajeDenuncia, FechaCreacion, Resuelta) VALUES (@IdCurso, @IdUsuario, @MensajeDenuncia, @FechaCreacion, @Resuelta)");
                accesoDatos.setearParametro("@IdCurso", idCurso);
                accesoDatos.setearParametro("@IdUsuario", idUsuario);
                accesoDatos.setearParametro("@MensajeDenuncia", mensajeDenuncia);
                accesoDatos.setearParametro("@FechaCreacion", DateTime.Now);
                accesoDatos.setearParametro("@Resuelta", false);

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

        public void DenunciaResuelta()
        {

        }

    }
}
