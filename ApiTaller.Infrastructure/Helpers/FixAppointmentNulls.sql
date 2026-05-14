USE tallermoto;

-- Permitir nulos en los campos de contacto de la cita, ya que son opcionales para clientes registrados
ALTER TABLE appointment MODIFY COLUMN contact_name VARCHAR(255) NULL;
ALTER TABLE appointment MODIFY COLUMN contact_phone VARCHAR(50) NULL;
ALTER TABLE appointment MODIFY COLUMN contact_email VARCHAR(255) NULL;
ALTER TABLE appointment MODIFY COLUMN vehicle_description VARCHAR(500) NULL;
