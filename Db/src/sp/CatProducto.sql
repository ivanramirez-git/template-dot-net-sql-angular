
-- Tabla CatProducto
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CatProducto' and xtype='U')
BEGIN
    CREATE TABLE CatProducto (
        Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        NombreProducto varchar(50) NOT NULL,
        ImagenProducto varchar(100) NULL,
        Precio decimal(18,2) NOT NULL,
        Ext varchar(5) NOT NULL,
        CreatedAt datetime DEFAULT GETDATE(),
        UpdatedAt datetime DEFAULT GETDATE(),
        DeletedAt datetime
    );
END

-- spGetAllCatProducto
-- spGetByIdCatProducto
-- spCreateCatProducto
-- spDeleteCatProducto
-- spUpdateCatProducto

-- spGetAllCatProducto
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetAllCatProducto' and xtype='P')
BEGIN
    DROP PROCEDURE spGetAllCatProducto;
END
GO
CREATE PROCEDURE spGetAllCatProducto
AS
BEGIN
    SELECT * FROM CatProducto WHERE DeletedAt IS NULL;
END
GO
-- EXEC spGetAllCatProducto;
-- spGetByIdCatProducto
IF EXISTS (SELECT * FROM sysobjects WHERE name='spGetByIdCatProducto' and xtype='P')
BEGIN
    DROP PROCEDURE spGetByIdCatProducto;
END
GO
CREATE PROCEDURE spGetByIdCatProducto
    @Id int
AS
BEGIN
    SELECT * FROM CatProducto WHERE Id = @Id AND DeletedAt IS NULL;
END
GO
-- EXEC spGetByIdCatProducto @Id = 1;
-- spCreateCatProducto
IF EXISTS (SELECT * FROM sysobjects WHERE name='spCreateCatProducto' and xtype='P')
BEGIN
    DROP PROCEDURE spCreateCatProducto;
END
GO
CREATE PROCEDURE spCreateCatProducto
    @Id int,
    @NombreProducto varchar(50),
    @ImagenProducto varchar(100),
    @Precio decimal(18,2),
    @Ext varchar(5)
AS
BEGIN
    -- Crear el producto
    INSERT INTO CatProducto (NombreProducto, ImagenProducto, Precio, Ext) VALUES (@NombreProducto, @ImagenProducto, @Precio, @Ext);
    -- Retornar el Row Insertado
    SELECT * FROM CatProducto WHERE Id = (SELECT MAX(Id) FROM CatProducto);
END
GO
-- EXEC spCreateCatProducto @NombreProducto = 'Producto 1', @ImagenProducto = 'Imagen 1', @Precio = 100.00, @Ext = 'jpg';
-- spDeleteCatProducto
IF EXISTS (SELECT * FROM sysobjects WHERE name='spDeleteCatProducto' and xtype='P')
BEGIN
    DROP PROCEDURE spDeleteCatProducto;
END
GO
CREATE PROCEDURE spDeleteCatProducto
    @Id int
AS
BEGIN
    -- Validar que el producto exista
    IF NOT EXISTS (SELECT * FROM CatProducto WHERE Id = @Id)
    BEGIN
        PRINT 'El producto no existe.';
        RETURN;
    END
    -- Validar que el producto no este Deleteado
    IF EXISTS (SELECT * FROM CatProducto WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El producto esta cancelado.';
        RETURN;
    END
    -- Eliminar el producto
    UPDATE CatProducto SET DeletedAt = GETDATE() WHERE Id = @Id;
    -- Retornar el producto eliminado
    SELECT * FROM CatProducto WHERE Id = @Id;
END
GO
-- EXEC spDeleteCatProducto @Id = 1;
-- spUpdateCatProducto
IF EXISTS (SELECT * FROM sysobjects WHERE name='spUpdateCatProducto' and xtype='P')
BEGIN
    DROP PROCEDURE spUpdateCatProducto;
END
GO
CREATE PROCEDURE spUpdateCatProducto
    @Id int,
    @NombreProducto varchar(50),
    @ImagenProducto varchar(100),
    @Precio decimal(18,2),
    @Ext varchar(5)
AS
BEGIN
    -- Validar que el producto exista
    IF NOT EXISTS (SELECT * FROM CatProducto WHERE Id = @Id)
    BEGIN
        PRINT 'El producto no existe.';
        RETURN;
    END
    -- Validar que el producto no este Deleteado
    IF EXISTS (SELECT * FROM CatProducto WHERE Id = @Id AND DeletedAt IS NOT NULL)
    BEGIN
        PRINT 'El producto esta cancelado.';
        RETURN;
    END
    -- Actualizar el producto
    UPDATE CatProducto SET NombreProducto = @NombreProducto, ImagenProducto = @ImagenProducto, Precio = @Precio, Ext = @Ext WHERE Id = @Id;
    -- Retornar el producto actualizado
    SELECT * FROM CatProducto WHERE Id = @Id;
END
GO
-- EXEC spUpdateCatProducto @Id = 1, @NombreProducto = 'Producto 1', @ImagenProducto = 'Imagen 1', @Precio = 100.00, @Ext = 'jpg';
