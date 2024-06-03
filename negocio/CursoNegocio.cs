using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

                    curso.Categorias = obtenerCategoriasDeCurso(curso.Id);

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

        public static List<Curso> listarCursosInscripto()
        {
            List<Curso> listarCurso = new List<Curso>();

            Datos accesoDatosCurso = new Datos();

            try
            {
                //Consulta para traer los cursos inscriptos
                /*SELECT C.Id, C.Nombre, C.Descripcion, C.UrlImagen, C.Estado
                    FROM Cursos C INNER JOIN Usuarios_Cursos UC
                    ON C.Id = UC.Id_Curso INNER JOIN Usuarios U
                    ON UC.Id_Usuario = U.Id
                    WHERE UC.AdquisicionConfirmada = 1*/
                accesoDatosCurso.setearConsulta("SELECT C.Id, C.Nombre, C.Descripcion, C.UrlImagen, C.Estado FROM Cursos C INNER JOIN Usuarios_Cursos UC ON C.Id = UC.Id_Curso INNER JOIN Usuarios U ON UC.Id_Usuario = U.Id WHERE UC.AdquisicionConfirmada = 1");

                accesoDatosCurso.ejecutarLectura();

                while (accesoDatosCurso.Lector.Read())
                {
                    Curso curso = new Curso();
                    curso.Id = (int)accesoDatosCurso.Lector["Id"];
                    curso.Nombre = (string)accesoDatosCurso.Lector["Nombre"];
                    curso.Descripcion = (string)accesoDatosCurso.Lector["Descripcion"];
                    curso.UrlImagen = (byte[])accesoDatosCurso.Lector["UrlImagen"];
                    //curso.ComentariosHabilitados = (bool)accesoDatosCurso.Lector["ComentarioHabilitado"];
                    //curso.Disponible = (bool)accesoDatosCurso.Lector["Disponible"];
                    curso.Activo = (bool)accesoDatosCurso.Lector["Estado"];

                    curso.Categorias = obtenerCategoriasDeCurso(curso.Id);

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

        public static List<Categoria> obtenerCategoriasDeCurso(int idCurso, bool soloActivas = true)
        {
            List<Categoria> listaCategorias = new List<Categoria>();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Id, Nombre , Imagen , Activo " + 
                    " FROM Categorias JOIN Cursos_Categorias ON Id = Id_Categoria " +
                    " WHERE Id_Curso = @IdCurso ";

                if (soloActivas)
                    consulta += " AND Activo = 1";

                datos.setearConsulta(consulta);

                datos.setearParametro("@IdCurso", idCurso);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)datos.Lector["Id"];
                    categoria.Nombre = (string)datos.Lector["Nombre"];
                    categoria.Imagen = (byte[])datos.Lector["Imagen"];
                    categoria.Activo = (bool)datos.Lector["Activo"];

                    listaCategorias.Add(categoria);
                }

                return listaCategorias;
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

        public static Curso obtenerCurso(int id)
        {
            Curso curso = new Curso();

            Datos datos = new Datos();

            try
            {
                datos.setearConsulta("SELECT Id, Id_UsuarioCreador, Nombre, Descripcion, " + 
                    " FechaPublicacion, Costo, Etiquetas, UrlImagen, ComentarioHabilitado, Disponible, Estado FROM Cursos " + 
                    " WHERE Id = @id");
                
                datos.setearParametro("@id", id);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    curso.Id = (int)datos.Lector["Id"];
                    curso.Nombre = (string)datos.Lector["Nombre"];
                    curso.Descripcion = (string)datos.Lector["Descripcion"];
                    curso.FechaPublicacion = (DateTime)datos.Lector["FechaPublicacion"];
                    curso.Costo = (decimal)datos.Lector["Costo"];
                    string etiquetas = (string)datos.Lector["Etiquetas"];
                    List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                    curso.Etiquetas = listaEtiquetas;
                    curso.UrlImagen = (byte[])datos.Lector["UrlImagen"];
                    curso.ComentariosHabilitados = (bool)datos.Lector["ComentarioHabilitado"];
                    curso.Disponible = (bool)datos.Lector["Disponible"];
                    curso.Activo = (bool)datos.Lector["Estado"];

                    curso.Categorias = obtenerCategoriasDeCurso(curso.Id);
                }

                return curso;
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
