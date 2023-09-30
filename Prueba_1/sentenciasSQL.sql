-- Genera las sentencias SQL que respondan lo siguiente:

--Depurar solo los ID diferentes de 6,7,9 y 10 de la tabla usuarios (5 puntos)
SELECT *
FROM usuarios
WHERE userId NOT IN (6, 7, 9, 10);

-- Actualizar el dato Sueldo en un 10 porciento a los empleados que tienen fechas entre el año 2000 y 2001 (5 puntos)
-- Run this part only once 
--UPDATE empleados
--SET Sueldo = Sueldo * 1.10
--WHERE YEAR(FechaIngreso) BETWEEN 2000 AND 2001;

SELECT *
FROM empleados
WHERE YEAR(FechaIngreso) BETWEEN 2000 AND 2001;

-- Realiza una consulta para traer el nombre de usuario y fecha de ingreso de los usuarios que gananen mas de 10000 y su apellido comience con T ordernado del mas reciente al mas antiguo (10 puntos)
SELECT u.Login, u.Nombre + ' ' + u.Paterno + ' ' + u.Materno AS NombreCompleto, e.FechaIngreso
FROM usuarios u
INNER JOIN empleados e ON u.userId = e.userId
WHERE e.Sueldo > 10000 AND u.Paterno LIKE 'T%'
ORDER BY e.FechaIngreso DESC;

-- Realiza una consulta donde agrupes a los empleados por sueldo, un grupo con los que ganan menos de 1200 y uno mayor o igual a 1200, cuantos hay en cada grupo? (10 puntos)
SELECT
    CASE
        WHEN e.Sueldo < 1200 THEN 'Menos de 1200'
        ELSE '1200 o más'
    END AS GrupoSueldo,
    COUNT(*) AS CantidadEmpleados
FROM empleados e
GROUP BY
    CASE
        WHEN e.Sueldo < 1200 THEN 'Menos de 1200'
        ELSE '1200 o más'
    END;

