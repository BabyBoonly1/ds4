USE master;
IF DB_ID('BenjaminBarrios') IS NULL
BEGIN
    CREATE DATABASE BenjaminBarrios;
END
GO

USE BenjaminBarrios;
GO

-- Tabla de usuarios 
CREATE TABLE BB_Usuarios (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioNombre NVARCHAR(50) NOT NULL UNIQUE,
    ContrasenaHash NVARCHAR(256) NOT NULL, 
    NombreCompleto NVARCHAR(100),
    Rol NVARCHAR(20) NOT NULL, -- 'Admin','Abogado','Asistente'
    Email NVARCHAR(100),
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Tabla de abogados 
CREATE TABLE BB_Abogados (
    AbogadoId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NULL, -- FK opcional a BB_Usuarios
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Telefono NVARCHAR(30)
);
GO

-- Clientes
CREATE TABLE BB_Clientes (
    ClienteId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(150) NOT NULL,
    Identificacion NVARCHAR(50), 
    Telefono NVARCHAR(50),
    Email NVARCHAR(100),
    Direccion NVARCHAR(250),
    Observaciones NVARCHAR(MAX)
);
GO

-- Casos 
CREATE TABLE BB_Casos (
    CasoId INT IDENTITY(1,1) PRIMARY KEY,
    CodigoCaso NVARCHAR(50) NOT NULL UNIQUE, 
    Titulo NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(MAX),
    FechaInicio DATETIME NOT NULL DEFAULT GETDATE(),
    FechaCierre DATETIME NULL,
    Estado NVARCHAR(50) NOT NULL, -- 'En Proceso','Archivado','Cerrado'
    AbogadoId INT NULL, 
    ClienteId INT NULL, 
    UltimaActualizacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO
-- FKs
ALTER TABLE BB_Casos
  ADD CONSTRAINT FK_BB_Casos_Abogado FOREIGN KEY (AbogadoId) REFERENCES BB_Abogados(AbogadoId);

ALTER TABLE BB_Casos
  ADD CONSTRAINT FK_BB_Casos_Cliente FOREIGN KEY (ClienteId) REFERENCES BB_Clientes(ClienteId);
GO

-- Documentos por caso
CREATE TABLE BB_Documentos (
    DocumentoId INT IDENTITY(1,1) PRIMARY KEY,
    CasoId INT NOT NULL,
    NombreOriginal NVARCHAR(260) NOT NULL,
    RutaArchivo NVARCHAR(500) NOT NULL, 
    Extension NVARCHAR(20),
    TamañoKB INT,
    SubidoPor INT NULL, 
    FechaSubida DATETIME NOT NULL DEFAULT GETDATE()
);
GO
ALTER TABLE BB_Documentos
  ADD CONSTRAINT FK_BB_Documentos_Caso FOREIGN KEY (CasoId) REFERENCES BB_Casos(CasoId);
GO

-- Audiencias / Reuniones
CREATE TABLE BB_Audiencias (
    AudienciaId INT IDENTITY(1,1) PRIMARY KEY,
    CasoId INT NOT NULL,
    Titulo NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(MAX),
    FechaHora DATETIME NOT NULL,
    Lugar NVARCHAR(250),
    CreadoPor INT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO
ALTER TABLE BB_Audiencias
  ADD CONSTRAINT FK_BB_Audiencias_Caso FOREIGN KEY (CasoId) REFERENCES BB_Casos(CasoId);
GO

-- Plazos / Tareas
CREATE TABLE BB_Plazos (
    PlazoId INT IDENTITY(1,1) PRIMARY KEY,
    CasoId INT NOT NULL,
    Titulo NVARCHAR(200) NOT NULL,
    FechaVencimiento DATETIME NOT NULL,
    RecordatorioDiasAntes INT NOT NULL DEFAULT 3, -- días antes a notificar
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Pendiente', -- 'Pendiente','Cumplido','Vencido'
    AsignadoA INT NULL, -- AbogadoId o UsuarioId
    Observaciones NVARCHAR(MAX)
);
GO
ALTER TABLE BB_Plazos
  ADD CONSTRAINT FK_BB_Plazos_Caso FOREIGN KEY (CasoId) REFERENCES BB_Casos(CasoId);
GO

-- Logs simples 
CREATE TABLE BB_Logs (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    Entidad NVARCHAR(50),
    EntidadId INT,
    UsuarioId INT,
    Accion NVARCHAR(50),
    Detalle NVARCHAR(MAX),
    Fecha DATETIME NOT NULL DEFAULT GETDATE()
);
GO

INSERT INTO BB_Usuarios (UsuarioNombre, ContrasenaHash, NombreCompleto, Rol, Email)
VALUES
('admin1', 'HASH_ADMIN_123', 'Administrador General', 'Admin', 'admin@bufete.com'),
('abogado1', 'HASH_ABOGADO_123', 'Lic. Carlos Mendoza', 'Abogado', 'carlos@bufete.com'),
('abogado2', 'HASH_ABOGADO_456', 'Lic. Maria Estrada', 'Abogado', 'maria@bufete.com'),
('asistente1', 'HASH_ASIST_123', 'Ana Torres', 'Asistente', 'ana@bufete.com');

INSERT INTO BB_Abogados (UsuarioId, Nombre, Email, Telefono)
VALUES
(2, 'Lic. Carlos Mendoza', 'carlos@bufete.com', '6000-1111'),
(3, 'Lic. Maria Estrada', 'maria@bufete.com', '6000-2222');

INSERT INTO BB_Clientes (Nombre, Identificacion, Telefono, Email, Direccion, Observaciones)
VALUES
('Juan Pérez', '8-123-456', '6200-9988', 'juanp@gmail.com', 'Ciudad de Panamá', 'Caso civil previo en 2021'),
('Empresa ABC S.A.', 'RUC 450123-1-789123', '390-1122', 'contacto@abc.com', 'Costa del Este', 'Cliente corporativo'),
('María López', '4-567-890', '6789-4433', 'marial@gmail.com', 'Arraiján', NULL);

INSERT INTO BB_Casos (CodigoCaso, Titulo, Descripcion, Estado, AbogadoId, ClienteId)
VALUES
('CAS-001', 'Demanda laboral contra Empresa XYZ',
 'Representación de Juan Pérez en demanda laboral por despido injustificado.',
 'En Proceso', 1, 1),

('CAS-002', 'Asesoría legal corporativa para Empresa ABC S.A.',
 'Revisión de contratos comerciales y cumplimiento normativo.',
 'En Proceso', 2, 2),

('CAS-003', 'Divorcio de mutuo acuerdo',
 'Representación de María López en proceso de divorcio exprés.',
 'Cerrado', 1, 3);

 INSERT INTO BB_Documentos (CasoId, NombreOriginal, RutaArchivo, Extension, TamañoKB, SubidoPor)
VALUES
(1, 'Contrato laboral.pdf',     '/docs/casos/1/contrato_laboral.pdf', 'pdf', 350, 2),
(1, 'Pruebas chat.png',         '/docs/casos/1/chats.png',            'png', 1200, 1),
(2, 'Contrato comercial.docx',  '/docs/casos/2/contrato.docx',        'docx', 180, 3),
(3, 'Acta de matrimonio.pdf',   '/docs/casos/3/acta_matrimonio.pdf',  'pdf', 500, 2);

INSERT INTO BB_Audiencias (CasoId, Titulo, Descripcion, FechaHora, Lugar, CreadoPor)
VALUES
(1, 'Audiencia preliminar', 'Presentación de pruebas iniciales.', '2025-01-15 09:00', 'Juzgado Laboral #3', 2),
(2, 'Reunión con junta directiva', 'Revisión de contratos.', '2025-02-10 14:30', 'Oficina ABC S.A.', 3),
(3, 'Firma de acuerdo', 'Acuerdo de divorcio.', '2024-11-25 10:00', 'Juzgado Civil #1', 2);

INSERT INTO BB_Plazos (CasoId, Titulo, FechaVencimiento, RecordatorioDiasAntes, Estado, AsignadoA, Observaciones)
VALUES
(1, 'Entrega de pruebas físicas', '2025-01-10', 5, 'Pendiente', 1, 'Pruebas deben incluir documentos firmados.'),
(2, 'Revisión final de contratos', '2025-02-05', 3, 'Pendiente', 2, NULL),
(3, 'Entrega de sentencia final', '2024-11-20', 2, 'Cumplido', 1, 'Documento archivado.');

INSERT INTO BB_Logs (Entidad, EntidadId, UsuarioId, Accion, Detalle)
VALUES
('Caso', 1, 2, 'Actualización', 'Se agregó documento de pruebas'),
('Plazo', 1, 1, 'Creación', 'Plazo creado para entrega de pruebas'),
('Caso', 3, 2, 'Cierre', 'Caso marcado como cerrado');

SELECT * 
FROM sys.tables;