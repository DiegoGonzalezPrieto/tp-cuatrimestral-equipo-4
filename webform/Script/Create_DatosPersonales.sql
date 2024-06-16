USE TP_Cuatrimestal_Equipo4

GO
EXEC sp_rename 'Usuarios.Nombre', 'UserName', 'COLUMN';
GO
CREATE TABLE DatosPersonales (
    IdUsuario INT NOT NULL PRIMARY KEY,
    Nombre VARCHAR(50) NULL,
    Apellido VARCHAR(50) NULL,
    Profesion VARCHAR(100) NULL,
    Provincia VARCHAR(100) NULL,
    Pais VARCHAR(100) NULL,
    FechaNacimiento DATE NULL,
    FotoPerfil VARCHAR(MAX) NULL,
    Biografia TEXT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(Id)
);
GO
ALTER procedure insertarUsuario
@email varchar(100),
@pass varchar(100),
@username varchar(50)
as
insert into Usuarios (Email, Pass, UserName, TipoUsuario, Estado) output inserted.id values (@email, @pass,@username, 0, 1)