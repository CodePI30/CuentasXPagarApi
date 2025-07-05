CREATE TRIGGER trg_log_usuarios_insert
ON usuarios
AFTER INSERT
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_nuevos)
    SELECT 'usuarios', 'INSERT', SYSTEM_USER, (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_usuarios_update
ON usuarios
AFTER UPDATE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores, datos_nuevos)
    SELECT 'usuarios', 'UPDATE', SYSTEM_USER,
           (SELECT * FROM DELETED FOR JSON AUTO),
           (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_usuarios_delete
ON usuarios
AFTER DELETE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores)
    SELECT 'usuarios', 'DELETE', SYSTEM_USER, (SELECT * FROM DELETED FOR JSON AUTO)
    FROM DELETED;
END;
GO


CREATE TRIGGER trg_log_conceptos_insert
ON conceptos
AFTER INSERT
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_nuevos)
    SELECT 'conceptos', 'INSERT', SYSTEM_USER, (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_conceptos_update
ON conceptos
AFTER UPDATE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores, datos_nuevos)
    SELECT 'conceptos', 'UPDATE', SYSTEM_USER,
           (SELECT * FROM DELETED FOR JSON AUTO),
           (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_conceptos_delete
ON conceptos
AFTER DELETE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores)
    SELECT 'conceptos', 'DELETE', SYSTEM_USER, (SELECT * FROM DELETED FOR JSON AUTO)
    FROM DELETED;
END;
GO


CREATE TRIGGER trg_log_proveedores_insert
ON proveedores
AFTER INSERT
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_nuevos)
    SELECT 'proveedores', 'INSERT', SYSTEM_USER, (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_proveedores_update
ON proveedores
AFTER UPDATE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores, datos_nuevos)
    SELECT 'proveedores', 'UPDATE', SYSTEM_USER,
           (SELECT * FROM DELETED FOR JSON AUTO),
           (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_proveedores_delete
ON proveedores
AFTER DELETE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores)
    SELECT 'proveedores', 'DELETE', SYSTEM_USER, (SELECT * FROM DELETED FOR JSON AUTO)
    FROM DELETED;
END;
GO


CREATE TRIGGER trg_log_documentos_insert
ON documentos_pagar
AFTER INSERT
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_nuevos)
    SELECT 'documentos_pagar', 'INSERT', SYSTEM_USER, (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_documentos_update
ON documentos_pagar
AFTER UPDATE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores, datos_nuevos)
    SELECT 'documentos_pagar', 'UPDATE', SYSTEM_USER,
           (SELECT * FROM DELETED FOR JSON AUTO),
           (SELECT * FROM INSERTED FOR JSON AUTO)
    FROM INSERTED;
END;
GO

CREATE TRIGGER trg_log_documentos_delete
ON documentos_pagar
AFTER DELETE
AS
BEGIN
    INSERT INTO logs_operaciones (tabla_afectada, tipo_operacion, usuario, datos_anteriores)
    SELECT 'documentos_pagar', 'DELETE', SYSTEM_USER, (SELECT * FROM DELETED FOR JSON AUTO)
    FROM DELETED;
END;
GO
