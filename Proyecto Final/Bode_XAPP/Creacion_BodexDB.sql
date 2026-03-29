-- =============================================
-- BASE DE DATOS: Bode_X
-- Sistema de Gestión de Ubicaciones en Bodega
-- =============================================

-- Crear la base de datos
CREATE DATABASE BodeX;
GO

USE BodeX;
GO

-- =============================================
-- TABLAS
-- =============================================

-- Tabla de Bodegas
CREATE TABLE Bodegas (
    BodegaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla de Ubicaciones (Pasillo-Estante-Nivel)
CREATE TABLE Ubicaciones (
    UbicacionID INT PRIMARY KEY IDENTITY(1,1),
    BodegaID INT NOT NULL,
    Pasillo NVARCHAR(10) NOT NULL, -- Ej: A, B, C
    Estante NVARCHAR(10) NOT NULL, -- Ej: 01, 02, 03
    Nivel NVARCHAR(10) NOT NULL,   -- Ej: 1, 2, 3
    Capacidad INT DEFAULT 100,     -- Capacidad en unidades
    Ocupado INT DEFAULT 0,         -- Unidades actualmente ocupadas
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Ubicaciones_Bodegas FOREIGN KEY (BodegaID) REFERENCES Bodegas(BodegaID),
    CONSTRAINT UQ_Ubicacion UNIQUE (BodegaID, Pasillo, Estante, Nivel)
);

-- Tabla de Categorías de Productos
CREATE TABLE Categorias (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

-- Tabla de Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    Codigo NVARCHAR(50) UNIQUE NOT NULL,
    Nombre NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500),
    CategoriaID INT,
    PrecioUnitario DECIMAL(10,2),
    UnidadMedida NVARCHAR(20), -- Ej: Unidad, Kg, Litro
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID)
);

-- Tabla de Inventario (Productos en Ubicaciones)
CREATE TABLE Inventario (
    InventarioID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    UbicacionID INT NOT NULL,
    Cantidad INT NOT NULL DEFAULT 0,
    FechaAsignacion DATETIME DEFAULT GETDATE(),
    FechaActualizacion DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Inventario_Productos FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    CONSTRAINT FK_Inventario_Ubicaciones FOREIGN KEY (UbicacionID) REFERENCES Ubicaciones(UbicacionID),
    CONSTRAINT UQ_Producto_Ubicacion UNIQUE (ProductoID, UbicacionID)
);

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - UBICACIONES
-- =============================================

-- SP: Obtener todas las ubicaciones
GO
CREATE PROCEDURE sp_Ubicaciones_GetAll
AS
BEGIN
    SELECT 
        u.UbicacionID,
        u.BodegaID,
        b.Nombre AS NombreBodega,
        u.Pasillo,
        u.Estante,
        u.Nivel,
        CONCAT(u.Pasillo, '-', u.Estante, '-', u.Nivel) AS CodigoUbicacion,
        u.Capacidad,
        u.Ocupado,
        (u.Capacidad - u.Ocupado) AS Disponible,
        CAST((u.Ocupado * 100.0 / NULLIF(u.Capacidad, 0)) AS DECIMAL(5,2)) AS PorcentajeOcupado,
        u.FechaCreacion
    FROM Ubicaciones u
    INNER JOIN Bodegas b ON u.BodegaID = b.BodegaID
    ORDER BY b.Nombre, u.Pasillo, u.Estante, u.Nivel;
END;
GO
GO

-- SP: Obtener una ubicación por ID
CREATE PROCEDURE sp_Ubicaciones_GetByID
    @UbicacionID INT
AS
BEGIN
    SELECT 
        u.UbicacionID,
        u.BodegaID,
        b.Nombre AS NombreBodega,
        u.Pasillo,
        u.Estante,
        u.Nivel,
        CONCAT(u.Pasillo, '-', u.Estante, '-', u.Nivel) AS CodigoUbicacion,
        u.Capacidad,
        u.Ocupado,
        u.FechaCreacion
    FROM Ubicaciones u
    INNER JOIN Bodegas b ON u.BodegaID = b.BodegaID
    WHERE u.UbicacionID = @UbicacionID;
END;
GO

-- SP: Insertar ubicación
CREATE PROCEDURE sp_Ubicaciones_Insert
    @BodegaID INT,
    @Pasillo NVARCHAR(10),
    @Estante NVARCHAR(10),
    @Nivel NVARCHAR(10),
    @Capacidad INT,
    @UbicacionID INT OUTPUT
AS
BEGIN
    BEGIN TRY
        INSERT INTO Ubicaciones (BodegaID, Pasillo, Estante, Nivel, Capacidad)
        VALUES (@BodegaID, @Pasillo, @Estante, @Nivel, @Capacidad);
        
        SET @UbicacionID = SCOPE_IDENTITY();
        SELECT @UbicacionID AS UbicacionID, 'SUCCESS' AS Result;
    END TRY
    BEGIN CATCH
        SELECT 0 AS UbicacionID, ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- SP: Actualizar ubicación
CREATE PROCEDURE sp_Ubicaciones_Update
    @UbicacionID INT,
    @BodegaID INT,
    @Pasillo NVARCHAR(10),
    @Estante NVARCHAR(10),
    @Nivel NVARCHAR(10),
    @Capacidad INT
AS
BEGIN
    BEGIN TRY
        UPDATE Ubicaciones
        SET BodegaID = @BodegaID,
            Pasillo = @Pasillo,
            Estante = @Estante,
            Nivel = @Nivel,
            Capacidad = @Capacidad
        WHERE UbicacionID = @UbicacionID;
        
        SELECT 'SUCCESS' AS Result;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- SP: Eliminar ubicación (lógico)
CREATE PROCEDURE sp_Ubicaciones_Delete
    @UbicacionID INT
AS
BEGIN
    BEGIN TRY
        -- Verificar si tiene productos asignados
        IF EXISTS (SELECT 1 FROM Inventario WHERE UbicacionID = @UbicacionID AND Cantidad > 0)
        BEGIN
            SELECT 'ERROR: No se puede eliminar una ubicación con productos asignados' AS Result;
            RETURN;
        END
        
        DELETE FROM Ubicaciones WHERE UbicacionID = @UbicacionID;
        
        SELECT 'SUCCESS' AS Result;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS - PRODUCTOS
-- =============================================

-- SP: Obtener productos con sus ubicaciones
CREATE PROCEDURE sp_Productos_GetWithUbicaciones
AS
BEGIN
    SELECT 
        p.ProductoID,
        p.Codigo,
        p.Nombre AS NombreProducto,
        p.Descripcion,
        c.Nombre AS Categoria,
        p.PrecioUnitario,
        p.UnidadMedida,
        u.UbicacionID,
        CONCAT(u.Pasillo, '-', u.Estante, '-', u.Nivel) AS CodigoUbicacion,
        b.Nombre AS NombreBodega,
        i.Cantidad,
        u.Capacidad,
        i.InventarioID
    FROM Productos p
    LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID
    LEFT JOIN Inventario i ON p.ProductoID = i.ProductoID
    LEFT JOIN Ubicaciones u ON i.UbicacionID = u.UbicacionID
    LEFT JOIN Bodegas b ON u.BodegaID = b.BodegaID
    ORDER BY p.Nombre, b.Nombre, u.Pasillo, u.Estante, u.Nivel;
END;
GO

-- SP: Asignar producto a ubicación
CREATE PROCEDURE sp_Inventario_AsignarProducto
    @ProductoID INT,
    @UbicacionID INT,
    @Cantidad INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Verificar capacidad disponible
        DECLARE @Disponible INT;
        SELECT @Disponible = (Capacidad - Ocupado) 
        FROM Ubicaciones 
        WHERE UbicacionID = @UbicacionID;
        
        IF @Cantidad > @Disponible
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT 'ERROR: Capacidad insuficiente en la ubicación' AS Result;
            RETURN;
        END
        
        -- Insertar o actualizar inventario
        IF EXISTS (SELECT 1 FROM Inventario WHERE ProductoID = @ProductoID AND UbicacionID = @UbicacionID)
        BEGIN
            UPDATE Inventario
            SET Cantidad = Cantidad + @Cantidad,
                FechaActualizacion = GETDATE()
            WHERE ProductoID = @ProductoID AND UbicacionID = @UbicacionID;
        END
        ELSE
        BEGIN
            INSERT INTO Inventario (ProductoID, UbicacionID, Cantidad)
            VALUES (@ProductoID, @UbicacionID, @Cantidad);
        END
        
        -- Actualizar ocupación de ubicación
        UPDATE Ubicaciones
        SET Ocupado = Ocupado + @Cantidad
        WHERE UbicacionID = @UbicacionID;
        
        COMMIT TRANSACTION;
        SELECT 'SUCCESS' AS Result;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- SP: Eliminar asignación de producto a ubicación
CREATE PROCEDURE sp_Inventario_EliminarAsignacion
    @InventarioID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Obtener datos de la asignación
        DECLARE @UbicacionID INT;
        DECLARE @Cantidad INT;
        
        SELECT @UbicacionID = UbicacionID, @Cantidad = Cantidad
        FROM Inventario
        WHERE InventarioID = @InventarioID;
        
        -- Eliminar de inventario
        DELETE FROM Inventario WHERE InventarioID = @InventarioID;
        
        -- Actualizar ocupación de ubicación
        UPDATE Ubicaciones
        SET Ocupado = Ocupado - @Cantidad
        WHERE UbicacionID = @UbicacionID;
        
        COMMIT TRANSACTION;
        SELECT 'SUCCESS' AS Result;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SELECT ERROR_MESSAGE() AS Result;
    END CATCH
END;
GO

-- =============================================
-- DATOS INICIALES
-- =============================================

-- Insertar bodega de ejemplo
INSERT INTO Bodegas (Nombre, Descripcion) VALUES ('Bodega Principal', 'Bodega central de almacenamiento');

-- Insertar categorías
INSERT INTO Categorias (Nombre, Descripcion) VALUES 
('Electrónica', 'Productos electrónicos'),
('Alimentos', 'Productos alimenticios'),
('Herramientas', 'Herramientas y equipos'),
('Oficina', 'Artículos de oficina');

-- Insertar ubicaciones de ejemplo
INSERT INTO Ubicaciones (BodegaID, Pasillo, Estante, Nivel, Capacidad) VALUES
(1, 'A', '01', '1', 100),
(1, 'A', '01', '2', 100),
(1, 'A', '02', '1', 150),
(1, 'B', '01', '1', 200),
(1, 'B', '01', '2', 200);

-- Insertar productos de ejemplo
INSERT INTO Productos (Codigo, Nombre, Descripcion, CategoriaID, PrecioUnitario, UnidadMedida) VALUES
('PROD001', 'Laptop Dell', 'Laptop Dell Inspiron 15', 1, 899.99, 'Unidad'),
('PROD002', 'Mouse Inalámbrico', 'Mouse Logitech M185', 1, 15.99, 'Unidad'),
('PROD003', 'Aceite de Oliva', 'Aceite extra virgen 1L', 2, 12.50, 'Litro'),
('PROD004', 'Taladro Eléctrico', 'Taladro DeWalt 20V', 3, 149.99, 'Unidad'),
('PROD005', 'Resma de Papel', 'Papel carta 500 hojas', 4, 5.99, 'Paquete');

-- Asignar algunos productos a ubicaciones
INSERT INTO Inventario (ProductoID, UbicacionID, Cantidad) VALUES
(1, 1, 25),
(2, 1, 50),
(3, 2, 30),
(4, 3, 15),
(5, 4, 100);

-- Actualizar ocupación
UPDATE Ubicaciones SET Ocupado = 75 WHERE UbicacionID = 1;
UPDATE Ubicaciones SET Ocupado = 30 WHERE UbicacionID = 2;
UPDATE Ubicaciones SET Ocupado = 15 WHERE UbicacionID = 3;
UPDATE Ubicaciones SET Ocupado = 100 WHERE UbicacionID = 4;

GO

PRINT 'Base de datos BodeX creada exitosamente';
GO
