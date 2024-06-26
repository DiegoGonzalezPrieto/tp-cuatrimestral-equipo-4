USE TP_Cuatrimestal_Equipo4
GO
ALTER TABLE Cursos 
ADD Duracion INT NOT NULL DEFAULT 12,
Fecha_Eliminacion_Final DATE NULL

UPDATE Cursos SET Duracion = 12;