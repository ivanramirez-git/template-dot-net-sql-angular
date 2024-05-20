-- SQL SERVER VERSION QUERY
SELECT @@VERSION;

PRINT 'Consultando la base de datos LabDev...';
-- Validar si existe la base de datos LabDev
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LabDev')
BEGIN
    -- Crear un inicio de sesión para el usuario developer
    USE master;
    PRINT 'Creando los inicios de sesión developer y cliente...';
    CREATE LOGIN developer WITH PASSWORD = 'abc123ABC';

    -- Crear un inicio de sesión para el usuario cliente
    CREATE LOGIN cliente WITH PASSWORD = 'abc123ABC';
    PRINT 'Inicios de sesión developer y cliente creados.';
    
    PRINT 'Creando la base de datos LabDev...';
    -- Crear la base de datos
    CREATE DATABASE LabDev;
    PRINT 'Base de datos LabDev creada.';

END
PRINT 'Fin de la consulta.';