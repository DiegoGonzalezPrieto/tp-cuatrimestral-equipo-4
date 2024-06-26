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
        public static List<Curso> listarCursos(bool disponible = true, bool estado = true)
        {
            List<Curso> listarCurso = new List<Curso>();

            Datos accesoDatosCurso = new Datos();

            try
            {
                string consulta = "SELECT C.Id, C.Id_UsuarioCreador, C.Nombre, C.Descripcion, C.FechaPublicacion, C.Costo, C.Etiquetas, C.UrlImagen, C.ComentarioHabilitado, C.Disponible, C.Estado, C.Suspencion, C.Duracion, U.UserName FROM Cursos  C INNER JOIN Usuarios U ON C.Id_UsuarioCreador = U.Id";
                if (disponible)
                    consulta += " WHERE C.Disponible = 1 AND C.Estado = 1 ";
                else if (estado)
                    consulta += " WHERE C.Estado = 1";


                accesoDatosCurso.setearConsulta(consulta);

                accesoDatosCurso.ejecutarLectura();

                while (accesoDatosCurso.Lector.Read())
                {
                    Curso curso = new Curso();
                    curso.Id = (int)accesoDatosCurso.Lector["Id"];
                    curso.IdUsuario = (int)accesoDatosCurso.Lector["Id_UsuarioCreador"];
                    curso.NombreUsuarioCreador = (string)accesoDatosCurso.Lector["UserName"];
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
                    //curso.Activo = (bool)accesoDatosCurso.Lector["Estado"];
                    curso.suspencionCurso = (bool)accesoDatosCurso.Lector["Suspencion"];
                    curso.Duracion = (int)accesoDatosCurso.Lector["Duracion"];

                    curso.Categorias = obtenerCategoriasDeCurso(curso.Id);

                    curso.Capitulos = obtenerCapitulosCurso(curso.Id);

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

        public static List<Curso> listarCursosEliminados()
        {
            List<Curso> listarCursoEliminados = new List<Curso>();

            Datos datosCursoEliminado = new Datos();

            try
            {
                string consulta = "SELECT C.Id, C.Id_UsuarioCreador, C.Nombre, C.Descripcion, C.FechaPublicacion, C.Costo, C.Etiquetas, C.UrlImagen, C.ComentarioHabilitado, C.Disponible, C.Estado, C.Duracion, U.UserName FROM Cursos  C INNER JOIN Usuarios U ON C.Id_UsuarioCreador = U.Id WHERE C.Estado = 0";

                datosCursoEliminado.setearConsulta(consulta);
                datosCursoEliminado.ejecutarLectura();

                while (datosCursoEliminado.Lector.Read())
                {
                    Curso curso = new Curso();
                    curso.Id = (int)datosCursoEliminado.Lector["Id"];
                    curso.IdUsuario = (int)datosCursoEliminado.Lector["Id_UsuarioCreador"];
                    curso.NombreUsuarioCreador = (string)datosCursoEliminado.Lector["UserName"];
                    curso.Nombre = (string)datosCursoEliminado.Lector["Nombre"];
                    curso.Descripcion = (string)datosCursoEliminado.Lector["Descripcion"];
                    curso.FechaPublicacion = (DateTime)datosCursoEliminado.Lector["FechaPublicacion"];
                    curso.Costo = (decimal)datosCursoEliminado.Lector["Costo"];
                    string etiquetas = (string)datosCursoEliminado.Lector["Etiquetas"];
                    List<string> listaEtiquetas = etiquetas.Split(',').ToList();
                    curso.Etiquetas = listaEtiquetas;
                    curso.UrlImagen = (byte[])datosCursoEliminado.Lector["UrlImagen"];
                    curso.ComentariosHabilitados = (bool)datosCursoEliminado.Lector["ComentarioHabilitado"];
                    curso.Disponible = (bool)datosCursoEliminado.Lector["Disponible"];
                    curso.Activo = (bool)datosCursoEliminado.Lector["Estado"];
                    curso.Duracion = (int)datosCursoEliminado.Lector["Duracion"];

                    listarCursoEliminados.Add(curso);

                    
                }
                return listarCursoEliminados;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCursoEliminado.cerrarConexion();
            }
        }

        public static List<Curso> listarCursosInscripto(int idUsuario)
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
                accesoDatosCurso.setearConsulta("SELECT C.Id, C.Nombre, C.Descripcion, C.UrlImagen, C.Estado, C.Duracion " +
                    " FROM Cursos C INNER JOIN Usuarios_Cursos UC ON C.Id = UC.Id_Curso " +
                    " WHERE UC.Id_Usuario = @idUsuario AND UC.AdquisicionConfirmada = 1");

                accesoDatosCurso.setearParametro("@idUsuario", idUsuario);

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
                    curso.Duracion = (int)accesoDatosCurso.Lector["Duracion"];

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

        public static List<Curso> listarCursosPorIdUsuario(int idUsuario, bool disponible = true, bool estado = true)
        {
            List<Curso> listarCurso = listarCursos(disponible, estado);
            return listarCurso.Where(curso => curso.IdUsuario == idUsuario).ToList();
        }

        public void agregarCurso(Curso nuevoCurso, List<int> idsCategorias)
        {
            Datos datosNuevoCurso = new Datos();

            try
            {
                datosNuevoCurso.setearConsulta("INSERT INTO Cursos (Id_UsuarioCreador, Nombre, Descripcion, FechaPublicacion, Costo, Etiquetas, UrlImagen, ComentarioHabilitado, Disponible, Estado, Duracion) " +
                    " OUTPUT inserted.Id VALUES (@IdUsuario, @Nombre, @Descripcion, getdate(), @Costo, @Etiquetas, @UrlImagen, @ComentarioHabilitado, @Disponible, 1, @Duracion)");
                datosNuevoCurso.setearParametro("@IdUsuario", nuevoCurso.IdUsuario);
                datosNuevoCurso.setearParametro("@Nombre", nuevoCurso.Nombre);
                datosNuevoCurso.setearParametro("@Descripcion", nuevoCurso.Descripcion);
                datosNuevoCurso.setearParametro("@Costo", nuevoCurso.Costo);
                string etiquetasConcatenadas = string.Join(";", nuevoCurso.Etiquetas);
                datosNuevoCurso.setearParametro("@Etiquetas", etiquetasConcatenadas);
                datosNuevoCurso.setearParametro("@UrlImagen", nuevoCurso.UrlImagen);
                datosNuevoCurso.setearParametro("@ComentarioHabilitado", nuevoCurso.ComentariosHabilitados);
                datosNuevoCurso.setearParametro("@Disponible", nuevoCurso.Disponible);
                datosNuevoCurso.setearParametro("@Duracion", nuevoCurso.Duracion);
                int idCurso = datosNuevoCurso.ejecturarAccionScalar();

                vincularCursoCategorias(idCurso, idsCategorias);
                estadisticaNuevoCurso(idCurso);
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


        public void modificarCurso(Curso Curso, List<int> idsCategorias)
        {
            Datos datosModificarCurso = new Datos();

            try
            {
                datosModificarCurso.setearConsulta("UPDATE Cursos SET Nombre = @Nombre, Descripcion = @Descripcion, Costo = @Costo, Etiquetas = @Etiquetas, UrlImagen = @UrlImagen, ComentarioHabilitado = @ComentarioHabilitado,  Disponible = @Disponible, Duracion = @Duracion WHERE Id = @Id");
                datosModificarCurso.setearParametro("@Nombre", Curso.Nombre);
                datosModificarCurso.setearParametro("@Descripcion", Curso.Descripcion);
                datosModificarCurso.setearParametro("@Costo", Curso.Costo);
                string etiquetasConcatenadas = string.Join(";", Curso.Etiquetas);
                datosModificarCurso.setearParametro("@Etiquetas", etiquetasConcatenadas);
                datosModificarCurso.setearParametro("@UrlImagen", Curso.UrlImagen);
                datosModificarCurso.setearParametro("@ComentarioHabilitado", Curso.ComentariosHabilitados);
                datosModificarCurso.setearParametro("@Disponible", Curso.Disponible);
                datosModificarCurso.setearParametro("@Id", Curso.Id);
                datosModificarCurso.setearParametro("@Duracion",Curso.Duracion);
                datosModificarCurso.ejecutarAccion();
                int idCurso = Curso.Id;

                vincularCursoCategorias(idCurso, idsCategorias);
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

        public static List<Capitulo> obtenerCapitulosCurso(int idCurso, bool soloActivas = true)
        {
            List<Capitulo> listaCapitulos = new List<Capitulo>();

            Datos datos = new Datos();

            try
            {
                string consulta = "SELECT Ca.Id, Ca.Id_Curso, Ca.Nombre, Ca.Orden, Ca.FechaCreacion, Ca.Activo, Ca.Liberado " +
                    " FROM Cursos Cu INNER JOIN Capitulos Ca ON Cu.Id = Ca.Id_Curso WHERE Cu.Id = @idCurso ";

                if (soloActivas)
                    consulta += " AND Activo = 1";

                consulta += " ORDER BY Ca.Orden ASC"; // para armar indice en orden

                datos.setearConsulta(consulta);

                datos.setearParametro("@idCurso", idCurso);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Capitulo capitulo = new Capitulo();
                    capitulo.Id = (int)datos.Lector["Id"];
                    capitulo.Nombre = (string)datos.Lector["Nombre"];
                    capitulo.Orden = (short)datos.Lector["Orden"];
                    capitulo.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    capitulo.Activo = (bool)datos.Lector["Activo"];
                    capitulo.Liberado = (bool)datos.Lector["Liberado"];


                    listaCapitulos.Add(capitulo);
                }

                return listaCapitulos;


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
                    " FechaPublicacion, Costo, Etiquetas, UrlImagen, ComentarioHabilitado, Disponible, Estado, Duracion FROM Cursos " +
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
                    curso.Duracion = (int)datos.Lector["Duracion"];

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

        public void estadisticaNuevoCurso(int idCurso)
        {
            Datos datosEstadisticaNuevoCurso = new Datos();

            try
            {
                string consulta = "INSERT INTO Estadisticas_Cursos (Id_Curso, CantidadAdquisiciones) VALUES (@idCurso, 0)";

                datosEstadisticaNuevoCurso.setearConsulta(consulta);
                datosEstadisticaNuevoCurso.setearParametro("idCurso", idCurso);
                datosEstadisticaNuevoCurso.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosEstadisticaNuevoCurso.cerrarConexion();
            }
        }

        public static void estadisticaCurso(int idCurso)
        {
            Datos datosEstadisticaCurso = new Datos();

            try
            {
                string consulta = "UPDATE Estadisticas_Cursos SET CantidadAdquisiciones += 1 WHERE Id_Curso = @idCurso";

                datosEstadisticaCurso.setearConsulta(consulta);
                datosEstadisticaCurso.setearParametro("@idCurso", idCurso);
                datosEstadisticaCurso.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosEstadisticaCurso.cerrarConexion();
            }
        }

        public static void desactivarCurso(int id)
        {
            Datos datosCurso = new Datos();

            try
            {
                datosCurso.setearConsulta("UPDATE Cursos SET Disponible = 0 WHERE Id = @Id");
                datosCurso.setearParametro("@Id", id);
                datosCurso.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCurso.cerrarConexion();
            }
        }
        public static void activarCurso(int id)
        {
            Datos datosCurso = new Datos();

            try
            {
                datosCurso.setearConsulta("UPDATE Cursos SET Disponible = 1 WHERE Id = @Id");
                datosCurso.setearParametro("@Id", id);
                datosCurso.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosCurso.cerrarConexion();
            }
        }

        public static void eliminarCurso(int idCurso)
        {
            //borrarCategoriasCurso(idCurso);

            Datos datosEliminarCurso = new Datos();

            //string consulta = "DELETE FROM Cursos WHERE Id = @IdCurso";
            string consulta = "UPDATE Cursos SET Disponible = 0, Estado = 0 WHERE Id = @IdCurso";
            try
            {
                datosEliminarCurso.setearConsulta(consulta);
                datosEliminarCurso.setearParametro("@IdCurso", idCurso);
                datosEliminarCurso.ejecutarLectura();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosEliminarCurso.cerrarConexion();
            }
        }

        public static Indice obtenerIndice(int idCurso)
        {
            Indice indice = new Indice();
            indice.Capitulos = new List<CapituloIndice>();
            List<Capitulo> capitulos = obtenerCapitulosCurso(idCurso);

            foreach (Capitulo cap in capitulos)
            {
                CapituloIndice capInd = new CapituloIndice();
                capInd.Nombre = cap.Nombre;
                capInd.Orden = cap.Orden;
                capInd.Contenidos = new List<ContenidoIndice>();

                List<Contenido> contenidos = CapituloNegocio.obtenerContenidosCapitulo(cap.Id);

                foreach (Contenido con in contenidos)
                {
                    ContenidoIndice conInd = new ContenidoIndice();
                    conInd.Id = con.Id;
                    conInd.Nombre = con.Nombre;
                    conInd.Orden = con.Orden;

                    capInd.Contenidos.Add(conInd);
                }
                indice.Capitulos.Add(capInd);
            }

            return indice;
        }

        public static List<Curso> obenerCursosPorCategoria(int idCategoria)
        {
            List<Curso> cursos = listarCursos(true, true);
            List<Curso> cursosDeCategoria = new List<Curso>();

            foreach (Curso c in cursos)
            {
                if (c.Categorias.Exists(cat => cat.Id == idCategoria))
                    cursosDeCategoria.Add(c);

            }
            return cursosDeCategoria;
        }

        public static void desactivarSuspencionCurso(int id)
        {
            Datos datosSuspencionCurso = new Datos();

            try
            {
                datosSuspencionCurso.setearConsulta("UPDATE Cursos SET Suspencion = 0 WHERE Id = @Id");
                datosSuspencionCurso.setearParametro("@Id", id);
                datosSuspencionCurso.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosSuspencionCurso.cerrarConexion();
            }
        }
        public static void activarSuspencionCurso(int id)
        {
            Datos datosSuspencionCurso = new Datos();

            try
            {
                datosSuspencionCurso.setearConsulta("UPDATE Cursos SET Suspencion = 1 WHERE Id = @Id");
                datosSuspencionCurso.setearParametro("@Id", id);
                datosSuspencionCurso.ejecutarLectura();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datosSuspencionCurso.cerrarConexion();
            }
        }
    }
}
