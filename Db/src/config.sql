PRINT 'Configurando la base de datos LabDev...';

-- Consultar si existe el usuario developer
IF NOT EXISTS (SELECT * FROM sys.sysusers WHERE name = 'developer')
BEGIN
    -- Cambiar al contexto de la base de datos
    USE LabDev;

    -- Asignar roles y permisos específicos a los usuarios

    -- Developer
    -- Crear un usuario developer para la base de datos
    CREATE USER developer FOR LOGIN developer;

    -- Permite leer todos los datos de todas las tablas
    ALTER ROLE db_datareader ADD MEMBER developer;
    -- Permite escribir todos los datos de todas las tablas
    ALTER ROLE db_datawriter ADD MEMBER developer;
    -- Permite crear, modificar y eliminar tablas
    ALTER ROLE db_ddladmin ADD MEMBER developer;
    -- Permite ejecutar todos los procedimientos almacenados
    GRANT EXECUTE TO developer;

    -- Cliente
    -- Crear un usuario cliente que solo pueda ejecutar procedimientos almacenados
    CREATE USER cliente FOR LOGIN cliente;

    -- Permite ejecutar todos los procedimientos almacenados
    GRANT EXECUTE TO cliente;
END

PRINT 'Base de datos LabDev creada correctamente.';