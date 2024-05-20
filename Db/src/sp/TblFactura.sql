
-- Tabla TblFactura
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TblFactura' and xtype='U')
BEGIN
    CREATE TABLE TblFactura (
        Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        FechaEmisionFactura datetime NOT NULL,
        IdCliente int NOT NULL,
        NumeroDeFactura int NOT NULL UNIQUE,
        NumeroDeProductos int NOT NULL,
        SubTotalFactura decimal(18,2) NOT NULL,
        TotalImpuestos decimal(18,2) NOT NULL,
        TotalFactura decimal(18,2) NOT NULL,
        CreatedAt datetime DEFAULT GETDATE(),
        UpdatedAt datetime DEFAULT GETDATE(),
        DeletedAt datetime,
        FOREIGN KEY (IdCliente) REFERENCES TblCliente(Id)
    );
END

-- spGetAllTblFactura
-- spGetByIdTblFactura
-- spCreateTblFactura ESTA CONTIENE TODA LA LOGICA PARA CREAR LA FACTURA, se deben pasar los datos minimos para crear la factura lo demas debe calcularse en el SP o se genera automaticamente en el SP
-- spUpdateTblFactura Esta solo recuenta el # de productos y recalcula los totales

-- spGetAllTblFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetAllTblFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spGetAllTblFactura;
END
GO
CREATE PROCEDURE spGetAllTblFactura
AS
BEGIN
    SELECT
        F.Id,
        F.FechaEmisionFactura,
        F.IdCliente,
        F.NumeroDeFactura,
        F.NumeroDeProductos,
        F.SubTotalFactura,
        F.TotalImpuestos,
        F.TotalFactura,
        F.CreatedAt,
        F.UpdatedAt,
        F.DeletedAt,
        C.Id AS Cliente_Id,
        C.RazonSocial AS Cliente_RazonSocial,
        C.IdTipoCliente AS Cliente_IdTipoCliente,
        C.FechaDeCreacion AS Cliente_FechaDeCreacion,
        C.RFC AS Cliente_RFC,
        C.CreatedAt AS Cliente_CreatedAt,
        C.UpdatedAt AS Cliente_UpdatedAt,
        C.DeletedAt AS Cliente_DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblFactura F
    LEFT JOIN TblCliente C ON F.IdCliente = C.Id
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE F.DeletedAt IS NULL;
END
GO
-- EXEC spGetAllTblFactura;
-- spGetByIdTblFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetByIdTblFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spGetByIdTblFactura;
END
GO
CREATE PROCEDURE spGetByIdTblFactura
    @Id int
AS
BEGIN
    SELECT
        F.Id,
        F.FechaEmisionFactura,
        F.IdCliente,
        F.NumeroDeFactura,
        F.NumeroDeProductos,
        F.SubTotalFactura,
        F.TotalImpuestos,
        F.TotalFactura,
        F.CreatedAt,
        F.UpdatedAt,
        F.DeletedAt,
        C.Id AS Cliente_Id,
        C.RazonSocial AS Cliente_RazonSocial,
        C.IdTipoCliente AS Cliente_IdTipoCliente,
        C.FechaDeCreacion AS Cliente_FechaDeCreacion,
        C.RFC AS Cliente_RFC,
        C.CreatedAt AS Cliente_CreatedAt,
        C.UpdatedAt AS Cliente_UpdatedAt,
        C.DeletedAt AS Cliente_DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblFactura F
    LEFT JOIN TblCliente C ON F.IdCliente = C.Id
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE F.Id = @Id AND F.DeletedAt IS NULL;
END
GO
-- EXEC spGetByIdTblFactura @Id = 1;
-- spCreateTblFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spCreateTblFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spCreateTblFactura;
END
GO
CREATE PROCEDURE spCreateTblFactura
    @Id int,
    @IdCliente int,
    @NumeroDeFactura int
AS
BEGIN
    -- Validar que el cliente exista
    IF NOT EXISTS (SELECT * FROM TblCliente WHERE Id = @IdCliente)
    BEGIN
        PRINT 'El cliente no existe.';
        RETURN;
    END
    -- Validar que el cliente no este Deleteado
    IF EXISTS (SELECT * FROM TblCliente WHERE Id = @IdCliente AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El cliente esta cancelado.';
        RETURN;
    END
    -- Crear la factura
    DECLARE @FechaEmisionFactura datetime = GETDATE();
    -- DECLARE @NumeroDeFactura int = (SELECT COUNT(*) FROM TblFactura) + 1;
    DECLARE @SubTotalFactura decimal(18,2) = (SELECT SUM(SubTotal) FROM TblDetalleFactura WHERE IdFactura = (SELECT MAX(Id) FROM TblFactura));
    DECLARE @TotalImpuestos decimal(18,2) = (@SubTotalFactura * 0.19);
    DECLARE @TotalFactura decimal(18,2) = (@SubTotalFactura + @TotalImpuestos);
    DECLARE @NumeroDeProductos int = (SELECT COUNT(*) FROM TblDetalleFactura WHERE IdFactura = (SELECT MAX(Id) FROM TblFactura)) + 0;
    -- Validar que en los valores calculados no existan NULL
    IF @SubTotalFactura IS NULL
    BEGIN
        SET @SubTotalFactura = 0;
    END
    IF @TotalImpuestos IS NULL
    BEGIN
        SET @TotalImpuestos = 0;
    END
    IF @TotalFactura IS NULL
    BEGIN
        SET @TotalFactura = 0;
    END
    IF @NumeroDeProductos IS NULL
    BEGIN
        SET @NumeroDeProductos = 0;
    END
    INSERT INTO TblFactura (FechaEmisionFactura, IdCliente, NumeroDeFactura, NumeroDeProductos, SubTotalFactura, TotalImpuestos, TotalFactura) VALUES (@FechaEmisionFactura, @IdCliente, @NumeroDeFactura, @NumeroDeProductos, @SubTotalFactura, @TotalImpuestos, @TotalFactura);
    -- Retornar el Row Insertado
    SET @Id = (SELECT MAX(Id) FROM TblFactura);
    EXEC spGetByIdTblFactura @Id;
END    
GO
-- EXEC spCreateTblFactura @IdCliente = 1;
-- spUpdateTblFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spUpdateTblFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spUpdateTblFactura;
END
GO
CREATE PROCEDURE spUpdateTblFactura
    @Id int
AS
BEGIN
    -- Validar que la factura exista
    IF NOT EXISTS (SELECT * FROM TblFactura WHERE Id = @Id)
    BEGIN
        PRINT 'La factura no existe.';
        RETURN;
    END
    -- Validar que la factura no este Deleteada
    IF EXISTS (SELECT * FROM TblFactura WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'La factura esta cancelada.';
        RETURN;
    END
    -- Actualizar la factura
    DECLARE @NumeroDeProductos int = (SELECT COUNT(*) FROM TblDetalleFactura WHERE IdFactura = @Id);
    DECLARE @SubTotalFactura decimal(18,2) = (SELECT SUM(SubTotal) FROM TblDetalleFactura WHERE IdFactura = @Id);
    DECLARE @TotalImpuestos decimal(18,2) = (@SubTotalFactura * 0.19);
    DECLARE @TotalFactura decimal(18,2) = (@SubTotalFactura + @TotalImpuestos);
    -- Validar que en los valores calculados no existan NULL
    IF @SubTotalFactura IS NULL
    BEGIN
        SET @SubTotalFactura = 0;
    END
    IF @TotalImpuestos IS NULL
    BEGIN
        SET @TotalImpuestos = 0;
    END
    IF @TotalFactura IS NULL
    BEGIN
        SET @TotalFactura = 0;
    END
    IF @NumeroDeProductos IS NULL
    BEGIN
        SET @NumeroDeProductos = 0;
    END
    UPDATE TblFactura SET NumeroDeProductos = @NumeroDeProductos, SubTotalFactura = @SubTotalFactura, TotalImpuestos = @TotalImpuestos, TotalFactura = @TotalFactura WHERE Id = @Id;
END
GO
-- EXEC spUpdateTblFactura @Id = 1;

-- spGetFacturasByCliente
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetFacturasByCliente' and xtype='P')
BEGIN
    DROP PROCEDURE spGetFacturasByCliente;
END
GO
CREATE PROCEDURE spGetFacturasByCliente
    @IdCliente int
AS
BEGIN
    SELECT
        F.Id,
        F.FechaEmisionFactura,
        F.IdCliente,
        F.NumeroDeFactura,
        F.NumeroDeProductos,
        F.SubTotalFactura,
        F.TotalImpuestos,
        F.TotalFactura,
        F.CreatedAt,
        F.UpdatedAt,
        F.DeletedAt,
        C.Id AS Cliente_Id,
        C.RazonSocial AS Cliente_RazonSocial,
        C.IdTipoCliente AS Cliente_IdTipoCliente,
        C.FechaDeCreacion AS Cliente_FechaDeCreacion,
        C.RFC AS Cliente_RFC,
        C.CreatedAt AS Cliente_CreatedAt,
        C.UpdatedAt AS Cliente_UpdatedAt,
        C.DeletedAt AS Cliente_DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblFactura F
    LEFT JOIN TblCliente C ON F.IdCliente = C.Id
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE F.IdCliente = @IdCliente AND F.DeletedAt IS NULL;
END
GO
-- EXEC spGetFacturasByCliente @IdCliente = 1;

-- spGetFacturasByNumeroFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetFacturasByNumeroFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spGetFacturasByNumeroFactura;
END
GO
CREATE PROCEDURE spGetFacturasByNumeroFactura
    @NumeroFactura int
AS
BEGIN
    SELECT
        F.Id,
        F.FechaEmisionFactura,
        F.IdCliente,
        F.NumeroDeFactura,
        F.NumeroDeProductos,
        F.SubTotalFactura,
        F.TotalImpuestos,
        F.TotalFactura,
        F.CreatedAt,
        F.UpdatedAt,
        F.DeletedAt,
        C.Id AS Cliente_Id,
        C.RazonSocial AS Cliente_RazonSocial,
        C.IdTipoCliente AS Cliente_IdTipoCliente,
        C.FechaDeCreacion AS Cliente_FechaDeCreacion,
        C.RFC AS Cliente_RFC,
        C.CreatedAt AS Cliente_CreatedAt,
        C.UpdatedAt AS Cliente_UpdatedAt,
        C.DeletedAt AS Cliente_DeletedAt,
        T.Id AS TipoCliente_Id,
        T.TipoCliente AS TipoCliente_TipoCliente,
        T.CreatedAt AS TipoCliente_CreatedAt,
        T.UpdatedAt AS TipoCliente_UpdatedAt,
        T.DeletedAt AS TipoCliente_DeletedAt
    FROM TblFactura F
    LEFT JOIN TblCliente C ON F.IdCliente = C.Id
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE F.NumeroDeFactura = @NumeroFactura AND F.DeletedAt IS NULL;
END
GO
-- EXEC spGetFacturasByNumeroFactura @NumeroFactura = 1;