-- Table: CatTipoCliente
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CatTipoCliente' and xtype='U')
BEGIN
    CREATE TABLE CatTipoCliente (
        Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        TipoCliente varchar(50) NOT NULL,
        CreatedAt datetime DEFAULT GETDATE(),
        UpdatedAt datetime DEFAULT GETDATE(),
        DeletedAt datetime
    );
END

-- spGetAllCatTipoCliente
-- spGetByIdCatTipoCliente
-- spCreateCatTipoCliente
-- spDeleteCatTipoCliente
-- spUpdateCatTipoCliente

-- spGetAllCatTipoCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetAllCatTipoCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spGetAllCatTipoCliente;
END
GO
CREATE PROCEDURE spGetAllCatTipoCliente
AS
BEGIN
    SELECT * FROM CatTipoCliente WHERE DeletedAt IS NULL;
END
GO
-- EXEC spGetAllCatTipoCliente;
-- spGetByIdCatTipoCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetByIdCatTipoCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spGetByIdCatTipoCliente;
END
GO
CREATE PROCEDURE spGetByIdCatTipoCliente
    @Id int
AS
BEGIN
    SELECT * FROM CatTipoCliente WHERE Id = @Id AND DeletedAt IS NULL;
END
GO
-- EXEC spGetByIdCatTipoCliente @Id = 1;
-- spCreateCatTipoCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spCreateCatTipoCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spCreateCatTipoCliente;
END
GO
CREATE PROCEDURE spCreateCatTipoCliente
    @Id int,
    @TipoCliente varchar(50)
AS
BEGIN
    -- Crear el tipo de cliente
    INSERT INTO CatTipoCliente (TipoCliente) VALUES (@TipoCliente);
    -- Retornar el Row Insertado
    SELECT * FROM CatTipoCliente WHERE Id = (SELECT MAX(Id) FROM CatTipoCliente);
END
GO
-- EXEC spCreateCatTipoCliente @TipoCliente = 'Tipo 1';
-- spDeleteCatTipoCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spDeleteCatTipoCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spDeleteCatTipoCliente;
END
GO
CREATE PROCEDURE spDeleteCatTipoCliente
    @Id int
AS
BEGIN
    -- Validar que el tipo de cliente exista
    IF NOT EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @Id)
    BEGIN
        PRINT 'El tipo de cliente no existe.';
        RETURN;
    END
    -- Validar que el tipo de cliente no este Deleteado
    IF EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El tipo de cliente esta cancelado.';
        RETURN;
    END
    -- Eliminar el tipo de cliente    
    UPDATE CatTipoCliente SET DeletedAt = GETDATE() WHERE Id = @Id;
    -- Retornar el tipo de cliente eliminado
    SELECT * FROM CatTipoCliente WHERE Id = @Id;
END
GO
-- EXEC spDeleteCatTipoCliente @Id = 1;
-- spUpdateCatTipoCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spUpdateCatTipoCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spUpdateCatTipoCliente;
END
GO
CREATE PROCEDURE spUpdateCatTipoCliente
    @Id int,
    @TipoCliente varchar(50)
AS
BEGIN
    -- Validar que el tipo de cliente exista
    IF NOT EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @Id)
    BEGIN
        PRINT 'El tipo de cliente no existe.';
        RETURN;
    END
    -- Validar que el tipo de cliente no este Deleteado
    IF EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El tipo de cliente esta cancelado.';
        RETURN;
    END
    -- Actualizar el tipo de cliente
    UPDATE CatTipoCliente SET TipoCliente = @TipoCliente WHERE Id = @Id;
    -- Retornar el tipo de cliente actualizado
    SELECT * FROM CatTipoCliente WHERE Id = @Id;
END
GO
-- EXEC spUpdateCatTipoCliente @Id = 1, @TipoCliente = 'Tipo 1';