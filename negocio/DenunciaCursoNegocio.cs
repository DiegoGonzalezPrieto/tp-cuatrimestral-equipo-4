using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class DenunciaCursoNegocio
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

        public static List<DenunciaCurso> ListarDenuncias(bool soloPendientes = false)
        {

            Datos datos = new Datos();

            List<DenunciaCurso> denuncias = new List<DenunciaCurso>();

            try
            {
                string consulta = "SELECT Id, Id_Curso, Id_UsuarioDenunciante, MensajeDenuncia, FechaCreacion, Resuelta " +
                    " FROM Denuncia_Cursos WHERE 1 = 1 ";

                if (soloPendientes)
                    consulta += " AND Resuelta = 0 ";

                datos.setearConsulta(consulta);
                
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DenunciaCurso denuncia = new DenunciaCurso();

                    denuncia.IdCurso = (int)datos.Lector["Id_Curso"];
                    denuncia.IdDenunciante = (int)datos.Lector["Id_UsuarioDenunciante"];
                    denuncia.MensajeDenuncia = (string)datos.Lector["MensajeDenuncia"];
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

        public void DenunciaResuelta()
        {

        }

    }
}
