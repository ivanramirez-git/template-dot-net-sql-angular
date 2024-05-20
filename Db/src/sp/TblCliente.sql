
-- Tabla TblCliente
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TblCliente' and xtype='U')
BEGIN
    CREATE TABLE TblCliente (
        Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        RazonSocial varchar(50) NOT NULL,
        IdTipoCliente int NOT NULL,
        FechaDeCreacion datetime DEFAULT GETDATE(),
        RFC varchar(50) NOT NULL,
        CreatedAt datetime DEFAULT GETDATE(),
        UpdatedAt datetime DEFAULT GETDATE(),
        DeletedAt datetime,
        FOREIGN KEY (IdTipoCliente) REFERENCES CatTipoCliente(Id)
    );
END

-- spGetAllTblCliente
-- spGetByIdTblCliente
-- spCreateTblCliente
-- spDeleteTblCliente
-- spUpdateTblCliente

-- spGetAllTblCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetAllTblCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spGetAllTblCliente;
END
GO
CREATE PROCEDURE spGetAllTblCliente
AS
BEGIN
    SELECT
        C.Id,
        C.RazonSocial,
        C.IdTipoCliente,
        C.FechaDeCreacion,
        C.RFC,
        C.CreatedAt,
        C.UpdatedAt,
        C.DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblCliente C
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE C.DeletedAt IS NULL;
END
GO
-- EXEC spGetAllTblCliente;
-- spGetByIdTblCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetByIdTblCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spGetByIdTblCliente;
END
GO
CREATE PROCEDURE spGetByIdTblCliente
    @Id int
AS
BEGIN
    SELECT
        C.Id,
        C.RazonSocial,
        C.IdTipoCliente,
        C.FechaDeCreacion,
        C.RFC,
        C.CreatedAt,
        C.UpdatedAt,
        C.DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblCliente C
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE C.Id = @Id AND C.DeletedAt IS NULL;
END
GO
-- EXEC spGetByIdTblCliente @Id = 1;
-- spCreateTblCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spCreateTblCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spCreateTblCliente;
END
GO
CREATE PROCEDURE spCreateTblCliente
    @Id int,
    @RazonSocial varchar(50),
    @IdTipoCliente int,
    @RFC varchar(50)
AS
BEGIN
    -- Validar que el tipo de cliente exista
    IF NOT EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @IdTipoCliente)
    BEGIN
        PRINT 'El tipo de cliente no existe.';
        RETURN;
    END
    -- Validar que el tipo de cliente no este Deleteado
    IF EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @IdTipoCliente AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El tipo de cliente esta cancelado.';
        RETURN;
    END
    -- Crear el cliente
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, RFC) VALUES (@RazonSocial, @IdTipoCliente, @RFC);
    -- Retornar el Row Insertado
    SET @Id = (SELECT MAX(Id) FROM TblCliente);
    EXEC spGetByIdTblCliente @Id;
END
GO
-- EXEC spCreateTblCliente @RazonSocial = 'Cliente 1', @IdTipoCliente = 1, @RFC = 'RFC 1';
-- spDeleteTblCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spDeleteTblCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spDeleteTblCliente;
END
GO
CREATE PROCEDURE spDeleteTblCliente
    @Id int
AS
BEGIN
    -- Validar que el cliente exista
    IF NOT EXISTS (SELECT * FROM TblCliente WHERE Id = @Id)
    BEGIN
        PRINT 'El cliente no existe.';
        RETURN;
    END
    -- Validar que el cliente no este Deleteado
    IF EXISTS (SELECT * FROM TblCliente WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El cliente esta cancelado.';
        RETURN;
    END
    -- Eliminar el cliente
    UPDATE TblCliente SET DeletedAt = GETDATE() WHERE Id = @Id;
    -- Retornar el cliente eliminado co todos los detalles de un cliente y su tipo de cliente
    SELECT
        C.Id,
        C.RazonSocial,
        C.IdTipoCliente,
        C.FechaDeCreacion,
        C.RFC,
        C.CreatedAt,
        C.UpdatedAt,
        C.DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblCliente C
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE C.Id = @Id;
END
GO
-- EXEC spDeleteTblCliente @Id = 1;
-- spUpdateTblCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spUpdateTblCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spUpdateTblCliente;
END
GO
CREATE PROCEDURE spUpdateTblCliente
    @Id int,
    @RazonSocial varchar(50),
    @IdTipoCliente int,
    @RFC varchar(50)
AS
BEGIN
    -- Validar que el cliente exista
    IF NOT EXISTS (SELECT * FROM TblCliente WHERE Id = @Id)
    BEGIN
        PRINT 'El cliente no existe.';
        RETURN;
    END
    -- Validar que el cliente no este Deleteado
    IF EXISTS (SELECT * FROM TblCliente WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El cliente esta cancelado.';
        RETURN;
    END
    -- Validar que el tipo de cliente exista
    IF NOT EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @IdTipoCliente)
    BEGIN
        PRINT 'El tipo de cliente no existe.';
        RETURN;
    END
    -- Validar que el tipo de cliente no este Deleteado
    IF EXISTS (SELECT * FROM CatTipoCliente WHERE Id = @IdTipoCliente AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El tipo de cliente esta cancelado.';
        RETURN;
    END
    -- Actualizar el cliente
    UPDATE TblCliente SET RazonSocial = @RazonSocial, IdTipoCliente = @IdTipoCliente, RFC = @RFC WHERE Id = @Id;
    -- Retornar el cliente actualizado
    EXEC spGetByIdTblCliente @Id;
END
GO
-- EXEC spUpdateTblCliente @Id = 1, @RazonSocial = 'Cliente 1', @IdTipoCliente = 1, @RFC = 'RFC 1';
