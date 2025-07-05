INSERT INTO usuarios (nombre_usuario, contrasena, rol, estado) VALUES
('admin', 'admin123', 'Admin', 1),
('jose', '123456', 'Usuario', 1),
('ana', 'clave123', 'Usuario', 1),
('maria', 'pass321', 'Usuario', 1),
('carlos', 'segura2023', 'Usuario', 1),
('luis', 'contraseña', 'Usuario', 1),
('lucia', 'adminuser', 'Usuario', 1),
('david', 'davidclave', 'Usuario', 1),
('karla', 'segura456', 'Usuario', 1),
('javier', 'miacceso', 'Usuario', 1);

INSERT INTO conceptos (descripcion, estado) VALUES
('Servicios de mantenimiento', 1),
('Compra de materiales', 1),
('Pago de transporte', 1),
('Suministros de oficina', 1),
('Pago de consultoría', 1),
('Servicios de limpieza', 1),
('Honorarios profesionales', 1),
('Alquiler de equipos', 1),
('Pago de energía eléctrica', 1),
('Publicidad y marketing', 1);

INSERT INTO proveedores (nombre, tipo_persona, cedula_rnc, balance, estado) VALUES
('Proveedor A', 'fisica', '131234567', 12000.00, 1),
('Proveedor B', 'fisica', '00123456789', 5000.00, 1),
('Servicios Lopez', 'fisica', '00234567891', 2500.00, 1),
('Soluciones SRL', 'fisica', '134567890', 8800.00, 1),
('Distribuidora ABC', 'fisica', '132345678', 13000.00, 1),
('Tecnicos RD', 'fisica', '00345678912', 4300.00, 1),
('Repuestos Juan', 'fisica', '00456789123', 2100.00, 1),
('Ofimatica Global', 'fisica', '135678901', 9500.00, 1),
('Consultores Asoc.', 'fisica', '136789012', 7200.00, 1),
('Logistica Express', 'fisica', '137890123', 6400.00, 1);

INSERT INTO documentos_pagar 
(numero_documento, numero_factura, fecha_documento, monto, proveedor_id, concepto_id, estado_pago) 
VALUES
('DOC-001', 'FAC-1001', '2024-06-01', 1200.00, 1, 1, 'Pendiente'),
('DOC-002', 'FAC-1002', '2024-06-02', 850.00, 2, 2, 'Pagado'),
('DOC-003', 'FAC-1003', '2024-06-03', 950.00, 3, 3, 'Pendiente');
