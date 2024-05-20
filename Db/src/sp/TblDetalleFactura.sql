
-- Tabla TblDetalleFactura
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TblDetalleFactura' and xtype='U')
BEGIN
    CREATE TABLE TblDetalleFactura (
        Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        IdFactura int NOT NULL,
        IdProducto int NOT NULL,
        CantidadDeProducto int NOT NULL,
        PrecioUnitario decimal(18,2) NOT NULL,
        SubTotal decimal(18,2) NOT NULL,
        Notas varchar(50) NULL,
        CreatedAt datetime DEFAULT GETDATE(),
        UpdatedAt datetime DEFAULT GETDATE(),
        DeletedAt datetime,
        FOREIGN KEY (IdFactura) REFERENCES TblFactura(Id),
        FOREIGN KEY (IdProducto) REFERENCES CatProducto(Id)
    );
END

-- spGetAllTblDetalleFactura
-- spGetByIdTblDetalleFactura
-- spCreateTblDetalleFactura ESTA CONTIENE TODA LA LOGICA PARA CREAR EL DETALLE DE LA FACTURA, se deben pasar los datos minimos para crear el detalle de la factura lo demas debe calcularse en el SP o se genera automaticamente en el SP Tambien se debe actualizar la factura con el SP spUpdateTblFactura

-- spGetAllTblDetalleFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetAllTblDetalleFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spGetAllTblDetalleFactura;
END
GO
CREATE PROCEDURE spGetAllTblDetalleFactura
AS
BEGIN
    SELECT
        D.Id,
        D.IdFactura,
        D.IdProducto,
        D.CantidadDeProducto,
        D.PrecioUnitario,
        D.SubTotal,
        D.Notas,
        D.CreatedAt,
        D.UpdatedAt,
        D.DeletedAt,
        F.FechaEmisionFactura,
        F.IdCliente,
        F.NumeroDeFactura,
        F.NumeroDeProductos,
        F.SubTotalFactura AS FacturaSubTotalFactura,
        F.TotalImpuestos AS FacturaTotalImpuestos,
        F.TotalFactura AS FacturaTotalFactura,
        F.CreatedAt AS FacturaCreatedAt,
        F.UpdatedAt AS FacturaUpdatedAt,
        F.DeletedAt AS FacturaDeletedAt,
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
    FROM TblDetalleFactura D
    LEFT JOIN TblFactura F ON D.IdFactura = F.Id
    LEFT JOIN TblCliente C ON F.IdCliente = C.Id
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE D.DeletedAt IS NULL;
END
GO
-- EXEC spGetAllTblDetalleFactura;
-- spGetByIdTblDetalleFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetByIdTblDetalleFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spGetByIdTblDetalleFactura;
END
GO
CREATE PROCEDURE spGetByIdTblDetalleFactura
    @Id int
AS
BEGIN
    SELECT
        D.Id,
        D.IdFactura,
        D.IdProducto,
        D.CantidadDeProducto,
        D.PrecioUnitario,
        D.SubTotal,
        D.Notas,
        D.CreatedAt,
        D.UpdatedAt,
        D.DeletedAt,
        F.FechaEmisionFactura,
        F.IdCliente,
        F.NumeroDeFactura,
        F.NumeroDeProductos,
        F.SubTotalFactura AS FacturaSubTotalFactura,
        F.TotalImpuestos AS FacturaTotalImpuestos,
        F.TotalFactura AS FacturaTotalFactura,
        F.CreatedAt AS FacturaCreatedAt,
        F.UpdatedAt AS FacturaUpdatedAt,
        F.DeletedAt AS FacturaDeletedAt,
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
    FROM TblDetalleFactura D
    LEFT JOIN TblFactura F ON D.IdFactura = F.Id
    LEFT JOIN TblCliente C ON F.IdCliente = C.Id
    LEFT JOIN CatTipoCliente T ON C.IdTipoCliente = T.Id
    WHERE D.Id = @Id AND D.DeletedAt IS NULL;
END
GO
-- EXEC spGetByIdTblDetalleFactura @Id = 1;
-- spCreateTblDetalleFactura
IF EXISTS (SELECT * FROM sysobjects WHERE name='spCreateTblDetalleFactura' and xtype='P')
BEGIN
    DROP PROCEDURE spCreateTblDetalleFactura;
END
GO
CREATE PROCEDURE spCreateTblDetalleFactura
    @Id int,
    @IdFactura int,
    @IdProducto int,
    @CantidadDeProducto int,
    @Notas varchar(50)
AS
BEGIN
    -- Validar que el producto exista
    IF NOT EXISTS (SELECT * FROM CatProducto WHERE Id = @IdProducto)
    BEGIN
        PRINT 'El producto no existe.';
        RETURN;
    END
    -- Validar que la factura exista
    IF NOT EXISTS (SELECT * FROM TblFactura WHERE Id = @IdFactura)
    BEGIN
        PRINT 'La factura no existe.';
        RETURN;
    END
    -- Validar que la factura no este Deleteada
    IF EXISTS (SELECT * FROM TblFactura WHERE Id = @IdFactura AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'La factura esta cancelada.';
        RETURN;
    END
    -- Validar que el producto no este Deleteado
    IF EXISTS (SELECT * FROM CatProducto WHERE Id = @IdProducto AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El producto esta cancelado.';
        RETURN;
    END
    -- Crear el detalle de la factura
    DECLARE @PrecioUnitario decimal(18,2) = (SELECT Precio FROM CatProducto WHERE Id = @IdProducto);
    DECLARE @SubTotal decimal(18,2) = (@PrecioUnitario * @CantidadDeProducto);
    -- Validar que en los valores calculados no existan NULL
    IF @PrecioUnitario IS NULL
    BEGIN
        SET @PrecioUnitario = 0;
    END
    IF @SubTotal IS NULL
    BEGIN
        SET @SubTotal = 0;
    END
    -- Crear el detalle de la factura
    INSERT INTO TblDetalleFactura (IdFactura, IdProducto, CantidadDeProducto, PrecioUnitario, SubTotal, Notas) VALUES (@IdFactura, @IdProducto, @CantidadDeProducto, @PrecioUnitario, @SubTotal, @Notas);
    EXEC spUpdateTblFactura @Id = @IdFactura;
    -- Retornar el Row Insertado
    SET @Id = (SELECT MAX(Id) FROM TblDetalleFactura);
    EXEC spGetByIdTblDetalleFactura @Id;
END
GO
-- EXEC spCreateTblDetalleFactura @IdFactura = 1, @IdProducto = 1, @CantidadDeProducto = 1, @Notas = 'Notas 1';
