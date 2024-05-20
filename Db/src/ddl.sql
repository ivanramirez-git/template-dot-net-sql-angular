PRINT 'Creando las tablas de la base de datos LabDev...';

-- Uso de la base de datos
USE LabDev;

-- Crear las tablas si no existen
-- Tabla CatTipoCliente
:r /docker-entrypoint-initdb.d/sp/CatTipoCliente.sql

-- Tabla TblCliente
:r /docker-entrypoint-initdb.d/sp/TblCliente.sql

-- Tabla CatProducto
:r /docker-entrypoint-initdb.d/sp/CatProducto.sql

-- Tabla TblFactura
:r /docker-entrypoint-initdb.d/sp/TblFactura.sql

-- Tabla TblDetalleFactura
:r /docker-entrypoint-initdb.d/sp/TblDetalleFactura.sql

PRINT 'Tablas de la base de datos LabDev creadas satisfactoriamente.';