
INSERT INTO usuarios (nombre_usuario, contrasena, rol, estado) VALUES
('admin', 'admin123', 'Admin', true),
('jose', '123456', 'Usuario', true),
('ana', 'clave123', 'Usuario', true),
('maria', 'pass321', 'Usuario', true),
('carlos', 'segura2023', 'Usuario', true),
('luis', 'contraseña', 'Usuario', true),
('lucia', 'adminuser', 'Usuario', true),
('david', 'davidclave', 'Usuario', true),
('karla', 'segura456', 'Usuario', true),
('javier', 'miacceso', 'Usuario', true);


INSERT INTO conceptos (descripcion, estado) VALUES
('Servicios de mantenimiento', true),
('Compra de materiales', true),
('Pago de transporte', true),
('Suministros de oficina', true),
('Pago de consultoría', true),
('Servicios de limpieza', true),
('Honorarios profesionales', true),
('Alquiler de equipos', true),
('Pago de energía eléctrica', true),
('Publicidad y marketing', true);


INSERT INTO proveedores (nombre, tipo_persona, cedula_rnc, balance, estado) VALUES
('Proveedor A', 'fisica', '131234567', 12000.00, true),
('Proveedor B', 'fisica', '00123456789', 5000.00, true),
('Servicios Lopez', 'fisica', '00234567891', 2500.00, true),
('Soluciones SRL', 'fisica', '134567890', 8800.00, true),
('Distribuidora ABC', 'fisica', '132345678', 13000.00, true),
('Tecnicos RD', 'fisica', '00345678912', 4300.00, true),
('Repuestos Juan', 'fisica', '00456789123', 2100.00, true),
('Ofimatica Global', 'fisica', '135678901', 9500.00, true),
('Consultores Asoc.', 'fisica', '136789012', 7200.00, true),
('Logistica Express', 'fisica', '137890123', 6400.00, true);


INSERT INTO documentos_pagar 
(numero_documento, numero_factura, fecha_documento, monto, proveedor_id, concepto_id, estado_pago) 
VALUES
('DOC-001', 'FAC-1001', '2024-06-01', 1200.00, 1, 1, 'Pendiente'),
('DOC-002', 'FAC-1002', '2024-06-02', 850.00, 2, 2, 'Pagado'),
('DOC-003', 'FAC-1003', '2024-06-03', 950.00, 3, 3, 'Pendiente');