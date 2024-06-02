CREATE DATABASE TP_Cuatrimestal_Equipo4
GO
USE TP_Cuatrimestal_Equipo4
GO 
CREATE TABLE Usuarios (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Email VARCHAR(100) NOT NULL,
    Pass VARCHAR(100) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    TipoUsuario BIT NOT NULL,
    FechaAlta DATETIME,
    Estado BIT NOT NULL
)
GO
CREATE TABLE Cursos (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_UsuarioCreador INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(250) NULL,
    FechaPublicacion DATETIME,
    Costo MONEY NULL,
    Etiquetas VARCHAR(50),
    UrlImagen VARBINARY(MAX),
    ComentarioHabilitado BIT NOT NULL,
    Disponible BIT NOT NULL,
    Estado BIT NOT NULL 
)
GO 
CREATE TABLE Usuarios_Cursos (
    Id_Curso INT NOT NULL FOREIGN KEY REFERENCES Cursos(Id),
    Id_Usuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    FechaAdquisicion DATETIME,
    AdquisicionConfirmada BIT NOT NULL,
    Estado BIT NOT NULL,
    PRIMARY KEY (Id_Curso, Id_Usuario)
)
GO 
CREATE TABLE Comentarios(
    ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Curso INT NOT NULL FOREIGN KEY REFERENCES Cursos(Id),
    Id_Usuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Mensaje VARCHAR(100) NULL,
    FechaCreacion DATETIME,
    Activo BIT NOT NULL
)
GO 
CREATE TABLE Capitulos(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Curso INT NOT NULL FOREIGN KEY REFERENCES Cursos(Id),
    Nombre VARCHAR(50) NOT NULL, 
    Orden SMALLINT NOT NULL, 
    FechaCreacion DATETIME,
    Activo BIT NOT NULL, 
    Liberado BIT NOT NULL
)
GO
CREATE TABLE TipoContenido(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
) 
GO
CREATE TABLE Contenidos(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Capitulo INT NOT NULL FOREIGN KEY REFERENCES Capitulos(Id),
    Nombre VARCHAR(50) NOT NULL, 
    Orden SMALLINT NOT NULL,
    TipoContenido INT NOT NULL FOREIGN KEY REFERENCES TipoContenido(Id),
    Texto VARCHAR(250) NULL,
    ArchivoPDF VARBINARY(MAX),
    FechaCreacion DATETIME,
    Activo BIT NOT NULL,
    Liberado BIT NOT NULL 
)
GO 
CREATE TABLE Usuarios_Contenidos_Completados (
    Id_Usuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Id_Contenido INT NOT NULL FOREIGN KEY REFERENCES Contenidos(Id),
    Completado BIT NOT NULL,
    PRIMARY KEY(Id_Usuario, Id_Contenido)
)
GO 
CREATE TABLE Estadisticas_Contenidos(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Contenido INT NOT NULL FOREIGN KEY REFERENCES Contenidos(Id),
    CantidadVisualizaciones SMALLINT NULL
)
GO 
CREATE TABLE Estadisticas_Cursos(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Curso INT NOT NULL FOREIGN KEY REFERENCES Cursos(Id),
    CantidadAdquisiciones SMALLINT
)
GO 
CREATE TABLE Categorias(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Imagen VARBINARY(MAX),
    Activo BIT NOT NULL
)
GO
CREATE TABLE Cursos_Categorias(
    Id_Curso INT NOT NULL REFERENCES Cursos(Id),
    Id_Categoria INT NOT NULL REFERENCES Categorias(Id),
    PRIMARY KEY (Id_Curso, Id_Categoria)
)
GO
CREATE TABLE Resenia(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Curso INT NOT NULL FOREIGN KEY REFERENCES Cursos(Id),
    Id_Usuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Puntaje SMALLINT,
    Mensaje VARCHAR(100),
    FechaCreacion DATETIME,
    Activo BIT NOT NULL
)
GO 
CREATE TABLE Denuncia_Resenia(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Resenia INT NOT NULL FOREIGN KEY REFERENCES Resenia(Id),
    Id_UsuarioDenunciante INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    Mensaje VARCHAR(100) NOT NULL,
    FechaCreacion DATETIME,
    Resuelta BIT NOT NULL   
)
GO
CREATE TABLE Denuncia_Cursos(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Id_Curso INT NOT NULL FOREIGN KEY REFERENCES Cursos(Id),
    Id_UsuarioDenunciante INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    MensajeDenuncia VARCHAR(100) NOT NULL,
    FechaCreacion DATETIME, 
    Resuelta BIT NOT NULL 
)