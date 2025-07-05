--Function to save logs
CREATE OR REPLACE FUNCTION log_operaciones()
RETURNS TRIGGER AS $$
BEGIN
    IF (TG_OP = 'DELETE') THEN
        INSERT INTO logs_operaciones(tabla_afectada, tipo_operacion, usuario, datos_anteriores)
        VALUES (TG_TABLE_NAME, TG_OP, current_user, row_to_json(OLD)::jsonb);
        RETURN OLD;
    ELSIF (TG_OP = 'UPDATE') THEN
        INSERT INTO logs_operaciones(tabla_afectada, tipo_operacion, usuario, datos_anteriores, datos_nuevos)
        VALUES (TG_TABLE_NAME, TG_OP, current_user, row_to_json(OLD)::jsonb, row_to_json(NEW)::jsonb);
        RETURN NEW;
    ELSIF (TG_OP = 'INSERT') THEN
        INSERT INTO logs_operaciones(tabla_afectada, tipo_operacion, usuario, datos_nuevos)
        VALUES (TG_TABLE_NAME, TG_OP, current_user, row_to_json(NEW)::jsonb);
        RETURN NEW;
    END IF;
END;
$$ LANGUAGE plpgsql;

--Setting up triggers

CREATE TRIGGER trg_log_usuarios
AFTER INSERT OR UPDATE OR DELETE ON usuarios
FOR EACH ROW EXECUTE FUNCTION log_operaciones();

CREATE TRIGGER trg_log_conceptos
AFTER INSERT OR UPDATE OR DELETE ON conceptos
FOR EACH ROW EXECUTE FUNCTION log_operaciones();

CREATE TRIGGER trg_log_proveedores
AFTER INSERT OR UPDATE OR DELETE ON proveedores
FOR EACH ROW EXECUTE FUNCTION log_operaciones();

CREATE TRIGGER trg_log_documentos
AFTER INSERT OR UPDATE OR DELETE ON documentos_pagar
FOR EACH ROW EXECUTE FUNCTION log_operaciones();



