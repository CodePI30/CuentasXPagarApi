create table usuarios(
id serial primary key,
nombre_usuario VARCHAR (50) NOT NULL UNIQUE,
contrasena VARCHAR (100) NOT NULL,
rol VARCHAR (20) CHECK (rol IN('Admin', 'Usuario')) NOT NULL,
estado BOOLEAN DEFAULT TRUE 
);

create table conceptos(
id serial Primary key,
descripcion TEXT NOT NULL,
estado BOOLEAN DEFAULT TRUE
);

create table proveedores(
id SERIAL PRIMARY KEY,
nombre VARCHAR (100) NOT NULL,
tipo_persona VARCHAR (20) CHECK (tipo_persona IN('fisica','juridica')) NOT NULL,
cedula_rnc VARCHAR (20) NOT NULL UNIQUE,
balance NUMERIC (12,2) DEFAULT 0.00,
estado BOOLEAN DEFAULT TRUE
);

CREATE TABLE documentos_pagar (
    id SERIAL PRIMARY KEY,
    numero_documento VARCHAR(20) NOT NULL UNIQUE,
    numero_factura VARCHAR(20) NOT NULL,
    fecha_documento DATE NOT NULL,
    monto NUMERIC(12,2) NOT NULL,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    proveedor_id INT NOT NULL REFERENCES proveedores(id),
    concepto_id INT NOT NULL REFERENCES conceptos(id),
    estado_pago VARCHAR(20) CHECK (estado_pago IN ('Pendiente', 'Pagado')) DEFAULT 'Pendiente'
);


CREATE TABLE logs_operaciones (
    id SERIAL PRIMARY KEY,
    tabla_afectada VARCHAR(50) NOT NULL,
    tipo_operacion VARCHAR(10) CHECK (tipo_operacion IN ('INSERT', 'UPDATE', 'DELETE')) NOT NULL,
    usuario TEXT,
    fecha_operacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    datos_anteriores JSONB,
    datos_nuevos JSONB
);

CREATE VIEW vista_balances_proveedores AS
SELECT 
    p.id AS proveedor_id,
    p.nombre,
    SUM(d.monto) FILTER (WHERE d.estado_pago = 'Pendiente') AS total_pendiente,
    SUM(d.monto) FILTER (WHERE d.estado_pago = 'Pagado') AS total_pagado,
    COUNT(d.id) AS cantidad_documentos
FROM proveedores p
LEFT JOIN documentos_pagar d ON d.proveedor_id = p.id
GROUP BY p.id, p.nombre;



