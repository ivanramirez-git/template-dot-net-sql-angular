PRINT 'Creando datos de prueba...';

USE LabDev;

-- Crear datos de prueba
-- Tabla CatTipoCliente
IF NOT EXISTS (SELECT * FROM CatTipoCliente)
BEGIN
    INSERT INTO CatTipoCliente (TipoCliente) VALUES ('Natural');
    INSERT INTO CatTipoCliente (TipoCliente) VALUES ('Jurídico');
END

-- Tabla TblCliente
IF NOT EXISTS (SELECT * FROM TblCliente)
BEGIN
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, FechaDeCreacion, RFC) VALUES ('Javier Salgado', 1, GETDATE(), '1113');
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, FechaDeCreacion, RFC) VALUES ('Sebastian Sepulveda', 1, GETDATE(), '1114');
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, FechaDeCreacion, RFC) VALUES ('Sebastian Reyes', 1, GETDATE(), '1118');
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, FechaDeCreacion, RFC) VALUES ('Ivan Ramirez', 1, GETDATE(), '1116');
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, FechaDeCreacion, RFC) VALUES ('Double V Partners', 2, GETDATE(), '1117');
    INSERT INTO TblCliente (RazonSocial, IdTipoCliente, FechaDeCreacion, RFC) VALUES ('Willis Towers Watson', 2, GETDATE(), '1115');
END

-- Tabla CatProducto
IF NOT EXISTS (SELECT * FROM CatProducto)
BEGIN
    INSERT INTO CatProducto (NombreProducto, ImagenProducto, Precio, Ext) VALUES ('Banano Criollo', 'https://images.rappi.com/products/0f173497-fba5-49a6-ad4b-5770f391bbec.png', 800.00, 'p1l2k');
    INSERT INTO CatProducto (NombreProducto, ImagenProducto, Precio, Ext) VALUES ('Limón Tahití', 'https://images.rappi.com/products/5a315f56-fa25-4a33-bdf7-134c49f52a93.png', 8300.00, 'a1s2d');
    INSERT INTO CatProducto (NombreProducto, ImagenProducto, Precio, Ext) VALUES ('Fresas Tipo Exportación', 'https://images.rappi.com/products/87fe80c1-ec24-4c8e-a11e-fed7f0e58cd8.jpg', 11900.00, 'w2e3r');
    INSERT INTO CatProducto (NombreProducto, ImagenProducto, Precio, Ext) VALUES ('Cebolla Cabezona Blanca', 'https://images.rappi.com/products/64b8fbe2-cccc-4da7-b27d-3bdb502f54f8.png', 1800.00, '2e3r4');
END

PRINT 'Datos de prueba creados con éxito.';