ALTER procedure [dbo].[insertarUsuario]
@email varchar(100),
@pass varchar(100),
@username varchar(50),
@fecha datetime
as
insert into Usuarios (Email, Pass, UserName, TipoUsuario, FechaAlta, Estado) output inserted.id values (@email, @pass,@username, 0, @fecha, 1)
