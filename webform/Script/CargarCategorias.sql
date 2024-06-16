USE TP_Cuatrimestal_Equipo4
GO

-- CAMBIAR LA RUTA POR LA DEL PROYECTO -- 
DECLARE @RutaProyecto NVARCHAR(255) = N'C:\Users\lucho\Desktop\Code\UTN\3°Cuatrimestre\Programacion III\tp-cuatrimestral-equipo-4\';

DECLARE @Instruccion NVARCHAR(MAX);

SET @Instruccion = N'
INSERT INTO Categorias (Nombre, Imagen, Activo)
VALUES 
(
    ''Marketing y Negocios'',
    (SELECT * FROM OPENROWSET(BULK ''' + @RutaProyecto + 'webform\Media\marketing.svg'', SINGLE_BLOB) AS Imagen),
    1
),
(
    ''Informatica y Software'',
    (SELECT * FROM OPENROWSET(BULK ''' + @RutaProyecto + 'webform\Media\software.svg'', SINGLE_BLOB) AS Imagen),
    1
),
(
    ''Desarrollo Personal'',
    (SELECT * FROM OPENROWSET(BULK ''' + @RutaProyecto + 'webform\Media\desarrolloPersonal.svg'', SINGLE_BLOB) AS Imagen),
    1
),
(
    ''Idiomas y Lenguas'',
    (SELECT * FROM OPENROWSET(BULK ''' + @RutaProyecto + 'webform\Media\idiomas.svg'', SINGLE_BLOB) AS Imagen),
    1
),
(
    ''Arte'',
    (SELECT * FROM OPENROWSET(BULK ''' + @RutaProyecto + 'webform\Media\arte.svg'', SINGLE_BLOB) AS Imagen),
    1
),
(
    ''Ciencia y Tecnologia'',
    (SELECT * FROM OPENROWSET(BULK ''' + @RutaProyecto + 'webform\Media\ciencia.svg'', SINGLE_BLOB) AS Imagen),
    1
);';

EXEC sp_executesql @Instruccion;
