-- Tabla usuarios
CREATE TABLE usuarios (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL UNIQUE,
    contrasena VARCHAR(100) NOT NULL,
    rol VARCHAR(20) NOT NULL CHECK (rol IN ('Admin', 'Usuario')),
    estado BIT DEFAULT 1
);

-- Tabla conceptos
CREATE TABLE conceptos (
    id INT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(MAX) NOT NULL,
    estado BIT DEFAULT 1
);

-- Tabla proveedores
CREATE TABLE proveedores (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    tipo_persona VARCHAR(20) NOT NULL CHECK (tipo_persona IN ('fisica', 'juridica')),
    cedula_rnc VARCHAR(20) NOT NULL UNIQUE,
    balance DECIMAL(12,2) DEFAULT 0.00,
    estado BIT DEFAULT 1
);

-- Tabla documentos_pagar
CREATE TABLE documentos_pagar (
    id INT IDENTITY(1,1) PRIMARY KEY,
    numero_documento VARCHAR(20) NOT NULL UNIQUE,
    numero_factura VARCHAR(20) NOT NULL,
    fecha_documento DATE NOT NULL,
    monto DECIMAL(12,2) NOT NULL,
    fecha_registro DATETIME DEFAULT GETDATE(),
    proveedor_id INT NOT NULL FOREIGN KEY REFERENCES proveedores(id),
    concepto_id INT NOT NULL FOREIGN KEY REFERENCES conceptos(id),
    estado_pago VARCHAR(20) DEFAULT 'Pendiente' CHECK (estado_pago IN ('Pendiente', 'Pagado'))
);

-- Tabla logs_operaciones
CREATE TABLE logs_operaciones (
    id INT IDENTITY(1,1) PRIMARY KEY,
    tabla_afectada VARCHAR(50) NOT NULL,
    tipo_operacion VARCHAR(10) NOT NULL CHECK (tipo_operacion IN ('INSERT', 'UPDATE', 'DELETE')),
    usuario VARCHAR(MAX),
    fecha_operacion DATETIME DEFAULT GETDATE(),
    datos_anteriores NVARCHAR(MAX),  -- Se puede guardar JSON como texto plano
    datos_nuevos NVARCHAR(MAX)
);

-- Vista vista_balances_proveedores
CREATE VIEW vista_balances_proveedores AS
SELECT 
    p.id AS proveedor_id,
    p.nombre,
    SUM(CASE WHEN d.estado_pago = 'Pendiente' THEN d.monto ELSE 0 END) AS total_pendiente,
    SUM(CASE WHEN d.estado_pago = 'Pagado' THEN d.monto ELSE 0 END) AS total_pagado,
    COUNT(d.id) AS cantidad_documentos
FROM proveedores p
LEFT JOIN documentos_pagar d ON d.proveedor_id = p.id
GROUP BY p.id, p.nombre;
