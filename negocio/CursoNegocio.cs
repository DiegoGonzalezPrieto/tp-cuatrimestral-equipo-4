using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CursoNegocio
    {
        public static List<Curso> listarCursos()
        {
            List<Curso> listarCurso = new List<Curso>();

            Datos accesoDatosCurso = new Datos();

            try
            {
                accesoDatosCurso.setearConsulta("SELECT Id, Id_UsuarioCreador, Nombre, Descripcion, FechaPublicacion, Costo, Etiquetas, UrlImagen, ComentarioHabilitado, Disponible, Estado FROM Cursos");

                accesoDatosCurso.ejecutarLectura();

                while (accesoDatosCurso.Lector.Read())
                {
                    Curso curso = new Curso();
                    curso.Id = (int)accesoDatosCurso.Lector["Id"];
                    curso.Nombre = (string)accesoDatosCurso.Lector["Nombre"];
                    curso.Descripcion = (string)accesoDatosCurso.Lector["Descripcion"];
                    curso.FechaPublicacion = (DateTime)accesoDatosCurso.Lector["FechaPublicacion"];
                    curso.Costo = (decimal)accesoDatosCurso.Lector["Costo"];
                    string etiquetas = (string)accesoDatosCurso.Lector["Etiquetas"];
                    List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                    curso.Etiquetas = listaEtiquetas;
                    curso.UrlImagen = (byte[])accesoDatosCurso.Lector["UrlImagen"];
                    curso.ComentariosHabilitados = (bool)accesoDatosCurso.Lector["ComentarioHabilitado"];
                    curso.Disponible = (bool)accesoDatosCurso.Lector["Disponible"];
                    curso.Activo = (bool)accesoDatosCurso.Lector["Estado"];

                    listarCurso.Add(curso);
                }

                return listarCurso;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatosCurso.cerrarConexion();
            }

        }

        public void agregarCurso(Curso nuevoCurso, List<int> idsCategorias)
        {
            Datos datosNuevoCurso = new Datos();

            try
            {
                datosNuevoCurso.setearConsulta("INSERT INTO Cursos (Id_UsuarioCreador, Nombre, Descripcion, FechaPublicacion, Costo, Etiquetas, UrlImagen, ComentarioHabilitado, Disponible, Estado) " +
                    " OUTPUT inserted.Id VALUES (1, @Nombre, @Descripcion, getdate(), @Costo, @Etiquetas, @UrlImagen, 1, 1, 1)");
                //datosNuevoCurso.setearParametro("@IdUsuarioCreador", nuevoCurso.IdUsuario);
                datosNuevoCurso.setearParametro("@Nombre", nuevoCurso.Nombre);
                datosNuevoCurso.setearParametro("@Descripcion", nuevoCurso.Descripcion);
                datosNuevoCurso.setearParametro("@Costo", nuevoCurso.Costo);
                string etiquetasConcatenadas = string.Join(";", nuevoCurso.Etiquetas);
                datosNuevoCurso.setearParametro("@Etiquetas", etiquetasConcatenadas);
                datosNuevoCurso.setearParametro("@UrlImagen", nuevoCurso.UrlImagen);
                //datosNuevoCurso.setearParametro("@Disponible", nuevoCurso.Disponible);
                int idCurso = datosNuevoCurso.ejecturarAccionScalar();

                vincularCursoCategorias(idCurso, idsCategorias);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosNuevoCurso.cerrarConexion();
            }

        }
       

        public static void modificarCurso(Curso Curso)
        {
            Datos datosModificarCurso = new Datos();

            try
            {
                datosModificarCurso.setearConsulta("UPDATE Cursos SET Nombre = @Nombre, Descripcion = @Descripcion, Costo = @Costo, Etiquetas = @Etiquetas, UrlImagen = @UrlImagen  Disponible = @Disponible WHERE Id = @Id");
                datosModificarCurso.setearParametro("@Nombre", Curso.Nombre);
                datosModificarCurso.setearParametro("@Descripcion", Curso.Descripcion);
                datosModificarCurso.setearParametro("@Costo", Curso.Costo);
                datosModificarCurso.setearParametro("@Etiquetas", Curso.Etiquetas);
                datosModificarCurso.setearParametro("@UrlImagen", Curso.UrlImagen);
                datosModificarCurso.setearParametro("@Disponible", Curso.Disponible);
                datosModificarCurso.setearParametro("@Id", Curso.Id);
                datosModificarCurso.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosModificarCurso.cerrarConexion();
            }
        }

        public void vincularCursoCategorias(int idCurso, List<int> idsCategorias)
        {
            Datos datosNuevoCurso = new Datos();

            if (idsCategorias.Count > 3)
                throw new Exception("Se ingresaron demasiadas categorías. Solo se permiten 3 por curso.");

            if (idsCategorias.Count == 0)
                return;

            borrarCategoriasCurso(idCurso);
            

            string consulta = "INSERT INTO Cursos_Categorias (Id_Curso, Id_Categoria) VALUES (@IdCurso, @IdCat1)";
            try
            {
                datosNuevoCurso.setearParametro("@IdCurso", idCurso);
                datosNuevoCurso.setearParametro("@IdCat1", idsCategorias[0]);

                if (idsCategorias.Count >= 2)
                {
                    consulta += ", (@IdCurso, @IdCat2)";
                    datosNuevoCurso.setearParametro("@IdCat2", idsCategorias[1]);
                }

                if (idsCategorias.Count == 3)
                {
                    consulta += ", (@IdCurso, @IdCat3)";
                    datosNuevoCurso.setearParametro("@IdCat3", idsCategorias[2]);
                }

                datosNuevoCurso.setearConsulta(consulta);
                datosNuevoCurso.ejecutarLectura();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosNuevoCurso.cerrarConexion();
            }

        }
        public static void borrarCategoriasCurso(int idCurso)
        {
            Datos datosNuevoCurso = new Datos();

            string consulta = "DELETE FROM Cursos_Categorias WHERE Id_Curso = @IdCurso";
            try
            {
                datosNuevoCurso.setearConsulta(consulta);
                datosNuevoCurso.setearParametro("@IdCurso", idCurso);
                datosNuevoCurso.ejecutarLectura();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosNuevoCurso.cerrarConexion();
            }
        }

    }
}
